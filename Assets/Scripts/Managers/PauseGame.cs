using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    public GameObject pauseMenuCanvas;
    public GameObject optionsMenuCanvas;

    public bool isPaused = false;

    void Start()
    {
    
    }

    void Update()
    {

        pauseGame();

    }

    public void pauseGame()
    {
        //Eðer ayarlar menüsü açýkken esc ye basarsak pause menüsü tekrar açýlýyor ve buga giriyor bu yüzden bu ifi yazdýk.
        if (optionsMenuCanvas.activeSelf)
            return;

        else if (Input.GetKeyUp(KeyCode.Escape) && !isPaused)
        {

            pauseMenuCanvas.SetActive(true);

            isPaused = true;

            Time.timeScale = 0;

        }

        else if (Input.GetKeyUp(KeyCode.Escape) && isPaused)
        {

            pauseMenuCanvas.SetActive(false);

            isPaused = false;

            Time.timeScale = 1;

        }

    }
}
