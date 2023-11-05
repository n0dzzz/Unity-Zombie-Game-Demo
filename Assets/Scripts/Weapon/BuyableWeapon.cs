using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyableWeapon : MonoBehaviour
{
    public TMP_Text weaponNameText;
    public TMP_Text weaponCostText;
    public int weaponCost;
    public string weaponName;
    public GameObject weaponObject;

    void Start()
    {
        weaponNameText.text = weaponName;
        weaponCostText.text = "$" + weaponCost;
    }
    
    public void buyWeapon()
    {
        if (PlayerController.Instance.playerMoney >= weaponCost)
        {
            weaponObject.SetActive(true);
            PlayerController.Instance.playerMoney -= weaponCost;
        }
    }
}
