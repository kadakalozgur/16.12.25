using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public PauseGame pauseGame;

    public void StartGame()
    {
        StartCoroutine(FindObjectOfType<loadingScreen>().showLoadingScreen("GameScene"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        pauseGame.isPaused = true;

        Time.timeScale = 0;
    }

    public void Home()
    {
        pauseGame.isPaused = false;

        SceneManager.LoadScene("MainMenu");

        Time.timeScale = 1;
    }

    public void Resume()
    {
        pauseGame.isPaused = false;

        Time.timeScale = 1;
    }

}
