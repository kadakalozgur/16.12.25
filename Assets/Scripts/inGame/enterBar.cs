using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Bu scripte oyuncu barýn kapýsýna temas ettiðinde ona bara girmek isteyip istemediðini soran
// bir menü gelir cevabýna göre oyun devam eder.

public class enterBar : MonoBehaviour
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
        StartCoroutine(FindObjectOfType<loadingScreen>().showLoadingScreen("BarScene")); // Yüklenme ekranýný çaðýrdýk.

    }

    public void noButton()
    {

        Time.timeScale = 1f;
        uiPanel.SetActive(false);
        Swordman.canMove = true;

    }
}
