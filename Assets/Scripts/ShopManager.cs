using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public int pricePredeter = 50;
    public int price = 50;
    private int multiplyPrice;
    public TextMeshProUGUI priceNumber;//
    public TextMeshProUGUI priceText;

    private PlayerManager playerManager;
    private bool playerIsInReack;

    public bool isHealthShop;
    public bool isAmmoShop;

    void Start() {
        priceText.text = price.ToString();

    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.E)) {
            BuyShop();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsInReack = true;
            multiplyPrice = other.transform.GetChild(2).GetComponent<GameManager>().round;
            price = pricePredeter * multiplyPrice;
            priceText.text = price.ToString();
            priceText.gameObject.SetActive(true);
            priceNumber.gameObject.SetActive(true);
            playerManager = other.GetComponent<PlayerManager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerIsInReack = false;
            priceText.gameObject.SetActive(false);
            priceNumber.gameObject.SetActive(false);
            playerManager = null;
        }
    }

    public void BuyShop() {
        if(playerIsInReack) {
            if(playerManager.totalPoints >= price) {
                playerManager.UpdatePoints(-price);//actualizo mis puntos en UI
                if (isHealthShop)
                {
                    playerManager.health = playerManager.healthCap;
                    playerManager.healthText.text = $"{playerManager.healthCap} HP";
                }
                if(isAmmoShop)
                {
                    foreach(Transform child in playerManager.weaponHolder.transform)
                    {
                        WeaponManager weaponManager = child.GetComponent<WeaponManager>();
                        weaponManager.currentAmmo = weaponManager.maxAmmo;
                        weaponManager.reserveAmmo = weaponManager.reserveAmmoCap;
                        StartCoroutine(weaponManager.Reload(weaponManager.reloadTime));

                    }
                }

            }
            else
            {
                print("No tienes puntos suficientes");
            }

        }

    }

}
