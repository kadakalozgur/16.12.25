using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bu script rulet UI kýsmýný kapatmak için yazýlmýþtýr.

public class closeRoulette : MonoBehaviour
{
    public GameObject moneyIconUI;
    public playRoulette playRoulette;
    public fileManagerMoney fileManagerMoney;

    public void closeTable()
    {

        this.gameObject.SetActive(false);
        moneyIconUI.SetActive(true);
        playRoulette.resetRouletteUI();

        fileManagerMoney.saveData();
        fileManagerMoney.loadData();
        fileManagerMoney.loadMoneyUI();

        Time.timeScale = 1f;
        Swordman.canMove = true;

    }
}
