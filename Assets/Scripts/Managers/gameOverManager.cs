using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverManager : MonoBehaviour
{
   public void respawn()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }
}
