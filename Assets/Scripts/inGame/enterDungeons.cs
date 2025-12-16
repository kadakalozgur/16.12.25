using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Bu scripte oyuncu kalenin kapýsýna temas ettiðinde ona kaleye girmek isteyip istemediðini soran
// bir menü gelir cevabýna göre oyun devam eder.

public class enterDungeons : MonoBehaviour
{
    public GameObject uiPanel;

    public int castleID; 

    void Start()
    {
        uiPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            DungeonManager.secilenKaleID = castleID;
            uiPanel.SetActive(true);
            Time.timeScale = 0f;
            Swordman.canMove = false;

        }
    }

    public void yesButton()
    {

        Time.timeScale = 1f;
        Swordman.canMove = false;
        StartCoroutine(FindObjectOfType<loadingScreen>().showLoadingScreen("DungeonScene")); // Yüklenme ekranýný çaðýrdýk.

    }

    public void noButton()
    {

        Time.timeScale = 1f;
        uiPanel.SetActive(false);
        Swordman.canMove = true;

    }
}