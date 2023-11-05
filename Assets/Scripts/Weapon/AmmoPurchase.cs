using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPurchase : MonoBehaviour
{
    public int ammoCost;
    
    public void buyAmmo()
    {
        if (PlayerController.Instance.playerMoney >= ammoCost)
        {
            //GunController.Instance.magReserve += GunController.Instance.magSize * 2;
            PlayerController.Instance.playerMoney -= ammoCost;
        }
    }
}
