using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text hpText;

    private void Update()
    {
        // Update the moneyText with the player's money value
        moneyText.text = "$" + PlayerController.Instance.playerMoney;
        hpText.text = "HP: " + PlayerController.Instance.playerHp;
    }
}
