using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Bu scripte rulet masasýndaki para UI'ý için güncelleme yapmak için yazýlmýþtýr.

public class rouletteMoneyUI : MonoBehaviour
{
    public TMP_Text ruletMoneyText;
    private fileManagerMoney moneyManager;

    private void Start()
    {
        moneyManager = FindObjectOfType<fileManagerMoney>();

        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {

        if (moneyManager != null && ruletMoneyText != null)
        {

            ruletMoneyText.text = moneyManager.para.ToString();

        }

    }
}
