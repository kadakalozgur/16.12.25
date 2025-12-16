using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// Bu scripte oyuncunun can verilerini tutmak için dosya iþlemleri yazýlmýþtýr.

public class fileManagerHealth : MonoBehaviour
{

    public Slider canSlider;  
    
    public float maksimumCan = 100f;
    public float mevcutCan = 100f;

    string klasorYolu;
    string dosyaYolu;

    private void Start()
    {
        klasorYolu = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "2D_RPG_GAME");
        dosyaYolu = Path.Combine(klasorYolu, "gameData(Health).txt");


        if (!Directory.Exists(klasorYolu))
        {
            Directory.CreateDirectory(klasorYolu);
        }

        loadData();

        updateHealthUI();
    }

    public void createFile()
    {
        saveData();
    }

    public void saveData()
    {
        string dataToSave = "Oyuncunun_Cani : " + mevcutCan.ToString();
        File.WriteAllText(dosyaYolu, dataToSave);
    }

    public void loadData()
    {
        if (!File.Exists(dosyaYolu))
        {
            mevcutCan = maksimumCan; 
            return;
        }

        string loadedData = File.ReadAllText(dosyaYolu);

        if (loadedData.StartsWith("Oyuncunun_Cani : "))
        {
            string healthString = loadedData.Replace("Oyuncunun_Cani : ", "");
            if (float.TryParse(healthString, out float loadedHealth))
            {
                mevcutCan = Mathf.Clamp(loadedHealth, 0, maksimumCan);
            }
            else
            {
                mevcutCan = maksimumCan;
            }
        }
    }
    private void OnApplicationQuit()
    {
        saveData();
    }

    public void updateHealthUI()
    {
        if (canSlider != null)
        {
            canSlider.value = mevcutCan / maksimumCan;
        }
    }
}
