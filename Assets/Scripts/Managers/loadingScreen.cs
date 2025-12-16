using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.BoolParameter;

//Bu scirpt sahneler arasýndaki geçiþ ekraný için yazýlmýþtýr.

public class loadingScreen : MonoBehaviour
{

    public GameObject loadingUI;

    private void Start()
    {
        
        loadingUI.SetActive(false);  

    }

    public IEnumerator showLoadingScreen(string sceneName)
    {

        loadingUI.SetActive(true);
        yield return new WaitForSecondsRealtime(1.2f); // 1.2 saniye delay koyduk.
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;

    }

}
