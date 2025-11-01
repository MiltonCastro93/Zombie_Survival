using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour{
    public GameObject playerCam;
    public float range = 100f;
    public float damage = 25f;
    public Animator playerAnimator;

    public ParticleSystem flashParticleSystem;
    public GameObject bloodParticleSystem;
    public GameObject concreteParticleSystem;

    public AudioClip shootClip;
    public AudioSource weaponAudioSource;

    public WeaponSway weaponSway;
    public float swaySensitivity;

    public GameObject crossHair;

    public float currentAmmo;
    public float maxAmmo;
    public float reloadTime;
    public bool isReloading;
    public float reserveAmmo;
    public float reserveAmmoCap;

    public TextMeshProUGUI currentAmmoText;
    public TextMeshProUGUI reserveAmmoText;

    public float fireRate;
    public float fireRateTimer;
    public bool isAutomatic;

    public string weaponType;

    public GameManager gameManager;

    private void Start()
    {
        weaponAudioSource = GetComponent<AudioSource>();
        swaySensitivity = weaponSway.swaySensitivity;

        UpdateAmmoTexts();

        reserveAmmoCap = reserveAmmo;
    }

    void Update()
    {
        if (!GameManager.sharedInstance.isPaused && !GameManager.sharedInstance.isGameOver)
        {
            if (playerAnimator.GetBool("isShooting"))
            {
                playerAnimator.SetBool("isShooting", false);
            }

            if (reserveAmmo <= 0 && currentAmmo <= 0)
            {
                Debug.Log("Te has quedado sin balas");
                return;
            }

            if (currentAmmo <= 0 && !isReloading)
            {
                Debug.Log("No tienes balas");
                StartCoroutine(Reload(reloadTime));
                return;
            }

            if (isReloading)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.R) && reserveAmmo > 0)
            {
                Debug.Log("Recarga manual de las balas");
                StartCoroutine(Reload(reloadTime));
                return;
            }

            if (fireRateTimer > 0)
            {
                fireRateTimer -= Time.deltaTime;
            }

            if (Input.GetButton("Fire1") && fireRateTimer <= 0 && isAutomatic)
            {
                Shoot();
                fireRateTimer = 1 / fireRate;
            }

            if (Input.GetButtonDown("Fire1") && fireRateTimer <= 0 && !isAutomatic)
            {
                Shoot();
                fireRateTimer = 1 / fireRate;
            }

            if (Input.GetButtonDown("Fire2"))
            {
                Aim();
            }

            if (Input.GetButtonUp("Fire2"))
            {
                if (playerAnimator.GetBool("isAiming"))
                {
                    playerAnimator.SetBool("isAiming", false);
                }

                weaponSway.swaySensitivity = swaySensitivity;
                crossHair.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        playerAnimator.SetTrigger(weaponType);

        UpdateAmmoTexts();

    }

    private void OnDisable()
    {
        playerAnimator.SetBool("isReloading", false);
        isReloading = false;
        Debug.Log("Recarga interrumpida por cambio de arma");
        if (!playerAnimator.GetBool("isAiming") && crossHair != null) {
            crossHair.SetActive(true);
        }
    }

    private void Shoot()
    {
        currentAmmo--;
        UpdateAmmoTexts();
        playerAnimator.SetBool("isShooting", true);
        flashParticleSystem.Play();
        weaponAudioSource.PlayOneShot(shootClip, 1);

        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range)) {
            EnemyManager enemyManager = hit.transform.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                enemyManager.Hit(damage);
                GameObject particleInstance = Instantiate(bloodParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
                particleInstance.transform.parent = hit.transform;
            }
            else
            {
                Instantiate(concreteParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }

    private void Aim()
    {
        playerAnimator.SetBool("isAiming", true);
        weaponSway.swaySensitivity = swaySensitivity / 100;
        crossHair.SetActive(false);
    }

    public IEnumerator Reload(float rt)
    {
        isReloading = true;
        playerAnimator.SetBool("isReloading", true);
        crossHair.SetActive(false);
        yield return new WaitForSeconds(rt);
        playerAnimator.SetBool("isReloading", false);
        if (!playerAnimator.GetBool("isAiming"))
        {
            crossHair.SetActive(true);
        }
        float missingAmmo = maxAmmo - currentAmmo;

        if (reserveAmmo >= missingAmmo)
        {
            currentAmmo += missingAmmo;
            reserveAmmo -= missingAmmo;
        }
        else
        {
            currentAmmo += reserveAmmo;
            reserveAmmo = 0;
        }

        if (gameObject.activeSelf)
        {
            UpdateAmmoTexts();
        }

        isReloading = false;
    }

    public void UpdateAmmoTexts()
    {
        currentAmmoText.text = currentAmmo.ToString();
        reserveAmmoText.text = reserveAmmo.ToString();
    }
}
