using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

//Bu scripte rulet oyununun oynanýþ kýsmý için yazýlmýþtýr.

public class playRoulette : MonoBehaviour
{
    public TMP_InputField betInputField;
    public TMP_Text moneyText;
    public TMP_Text infoText;
    public GameObject rouletteWheel;

    public Button redButton;
    public Button blackButton;
    public Button greenButton;
    public Button exitButton;
    public Button spinButton;
    public stopArrow arrow;

    private fileManagerMoney moneyManager;
    private rouletteMoneyUI moneyUI;

    private int bet = 0;
    private string selectColor = "";

    private void Start()
    {

        moneyManager = FindObjectOfType<fileManagerMoney>();
        moneyUI = FindObjectOfType<rouletteMoneyUI>();

        moneyUI.UpdateMoneyUI();

        redButton.onClick.AddListener(() => placeBet("red"));
        blackButton.onClick.AddListener(() => placeBet("black"));
        greenButton.onClick.AddListener(() => placeBet("green"));

        spinButton.interactable = false;

    }

    void placeBet(string color)
    {

        if (moneyManager == null)
            return;

        string inputTextBox = betInputField.text.Trim();

        if (string.IsNullOrEmpty(inputTextBox))
        {
            infoText.text = "Bet amount cannot be empty!";
            return;
        }

        if (!int.TryParse(inputTextBox, out bet))
        {
            infoText.text = "The bet amount can only be one number!";
            return;
        }

        if (bet <= 0)
        {
            infoText.text = "Please enter a valid amount!";
            return;
        }

        if (bet > moneyManager.para)
        {
            infoText.text = "Insufficient money!";
            return;
        }

        if (string.IsNullOrEmpty(color))
        {
            infoText.text = "Choose a color!";
            return;
        }

        selectColor = color;

        redButton.interactable = false;
        blackButton.interactable = false;
        greenButton.interactable = false;
        exitButton.interactable= false;
        spinButton.interactable = true;

    }

    public void startGame()
    {
        spinButton.interactable = false;

        moneyManager.para -= bet;
        moneyManager.saveData();
        moneyUI.UpdateMoneyUI();

        arrow.color = "";
        infoText.text = "";

        StartCoroutine(RotateWheel());
    }

    private IEnumerator RotateWheel()
    {
        float randomStartRotation = Random.Range(0f, 360f);

        rouletteWheel.transform.rotation = Quaternion.Euler(0, 0, randomStartRotation);

        float spinTime = 6f;
        float elapsed = 0f;
        float startSpeed = 2500f;
        float endSpeed = 0f;

        while (elapsed < spinTime)
        {
            float t = elapsed / spinTime;
            float currentSpeed = Mathf.SmoothStep(startSpeed, endSpeed, t);
            float deltaRotation = currentSpeed * Time.deltaTime;

            rouletteWheel.transform.Rotate(0, 0, deltaRotation);

            elapsed += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        endGame();
    }

    public void endGame()
    {

        string winColor = arrow.color;

        if (string.IsNullOrEmpty(winColor))
        {
            resetRouletteUI();
            return;
        }

        infoText.text = "Result : " + winColor.ToUpper();

        if (winColor == selectColor)
        {
            int winAmount = bet;

            if (winColor == "green")
                    winAmount *= 14;

            else
                winAmount *= 2;

            moneyManager.para += winAmount;

            infoText.text += " You WON " + winAmount + " Gold!";
        }

        else
        {
            infoText.text += " You lost " + bet + " Gold!";
        }

        moneyManager.saveData();
        moneyUI.UpdateMoneyUI();

        Invoke(nameof(resetRouletteUI), 2f); // 2 saniye bekettik.
    }


    public void resetRouletteUI()
    {

        betInputField.text = "";          
        infoText.text = "";              
        selectColor = "";               
        bet = 0;

        redButton.interactable = true;
        blackButton.interactable = true;
        greenButton.interactable = true;
        exitButton.interactable = true;
        spinButton.interactable = false;

    }
}

