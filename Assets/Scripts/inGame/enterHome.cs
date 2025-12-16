using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Bu scripte oyuncu kendi evinin kapýsýna temas ettiðinde ona eve girmek isteyip istemediðini soran
// bir menü gelir cevabýna göre oyun devam eder.

public class enterHome : MonoBehaviour
{
    public GameObject uiPanel;

    void Start()
    {
        uiPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
    
            uiPanel.SetActive(true);
            Time.timeScale = 0f;
            Swordman.canMove = false;

        }
    }

    public void yesButton()
    {

        Time.timeScale = 1f;
        Swordman.canMove = false;
        StartCoroutine(FindObjectOfType<loadingScreen>().showLoadingScreen("HouseScene")); // Yüklenme ekranýný çaðýrdýk.

    }

    public void noButton()
    {

        Time.timeScale = 1f;
        uiPanel.SetActive(false);
        Swordman.canMove = true;

    }
}
