using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour {
    public float health = 100f;
    public float healthCap;

    public TextMeshProUGUI healthText;
    public GameManager gameManager;

    public GameObject playerCamera;
    public CanvasGroup hitPanel;

    private float shakeTime = 1f;
    private float shakeDuration = 0.5f;
    private Quaternion playerCameraOriginalRotation;

    public GameObject weaponHolder;
    private int activeWeaponIndex;
    private GameObject activeWeapon;

    public int totalPoints;
    public TextMeshProUGUI pointsTexts;

    private void Start()
    {
        playerCameraOriginalRotation = playerCamera.transform.localRotation;

        WeaponSwitch(0);
        totalPoints = 0;
        UpdatePoints(0);

        healthCap = health;
    }

    private void Update()
    {
        if (hitPanel.alpha > 0)
        {
            hitPanel.alpha -= Time.deltaTime;
        }

        if (shakeTime < shakeDuration)
        {
            shakeTime += Time.deltaTime;
            CameraShake();
        }
        else if (playerCamera.transform.localRotation != playerCameraOriginalRotation)
        {
            playerCamera.transform.localRotation = playerCameraOriginalRotation;
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0 || Input.GetKeyDown(KeyCode.Q))
        {
            WeaponSwitch(activeWeaponIndex + 1);
        }

    }

    public void Hit(float damage)
    {
        health -= damage;
        healthText.text = $"{health} HP";

        if (health <= 0)
        {
            gameManager.GameOver();
        }
        else
        {
            shakeTime = 0;
            hitPanel.alpha = 1f;
        }
    }

    public void CameraShake()
    {
        playerCamera.transform.localRotation = Quaternion.Euler(Random.Range(-2f, 2f), 0, 0);
    }

    public void WeaponSwitch(int weaponIndex)
    {
        int index = 0;
        int amountOfWeapons = weaponHolder.transform.childCount;

        if (weaponIndex > amountOfWeapons - 1)
        {
            weaponIndex = 0;
        }

        foreach (Transform child in weaponHolder.transform)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }

            if (index == weaponIndex)
            {
                child.gameObject.SetActive(true);
                activeWeapon = child.gameObject;
            }

            index++;
        }

        activeWeaponIndex = weaponIndex;
    }

    public void UpdatePoints(int pointsToAdd)
    {
        totalPoints += pointsToAdd;
        pointsTexts.text = $"Points: {totalPoints}";
    }

}
