using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Bu scripte oyuncu kendi evinin kapýsýna temas ettiðinde ona evden çýkmak isteyip istemediðini soran
// bir menü gelir cevabýna göre oyun devam eder.

public class exitHome : MonoBehaviour
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

        spawnPlayer.karakterinSonKonumu = "outHouse"; // Karakterin spawn noktasý için yer belirttik.
        Time.timeScale = 1f;
        Swordman.canMove = false;
        StartCoroutine(FindObjectOfType<loadingScreen>().showLoadingScreen("GameScene")); // Yüklenme ekranýný çaðýrdýk.

    }

    public void noButton()
    {
        Time.timeScale = 1f;
        uiPanel.SetActive(false);
        Swordman.canMove = true;

    }
}
