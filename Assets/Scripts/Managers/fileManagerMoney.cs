using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

//Bu scripte oyuncu verilerini tutmak için dosya iþlemleri yazýlmýþtýr.

public class fileManagerMoney : MonoBehaviour
{
    public TMP_Text paraText;
    public int para = 0;

    string klasorYolu;
    string dosyaYolu;
    private void Start()
    {
        klasorYolu = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "2D_RPG_GAME");
        dosyaYolu = Path.Combine(klasorYolu, "gameData(Money).txt");

        if (!Directory.Exists(klasorYolu))
        {
            Directory.CreateDirectory(klasorYolu); // Eðer klasör yoksa oluþturudk oyuncu ilk giridðidne klasör yok çünkü
        }

        loadData();

        loadMoneyUI();
    }
    public void createFile()
    {
        saveData();
    }

    public void saveData()
    {
        string dataToSave = "Oyuncunun_Parasi : " + para.ToString();

        File.WriteAllText(dosyaYolu, dataToSave);
    }

    public void loadData()
    {
        if (!File.Exists(dosyaYolu))
        {
            return;
        }

        string loadedData = File.ReadAllText(dosyaYolu);

        if (loadedData.StartsWith("Oyuncunun_Parasi : "))
        {

            string moneyString = loadedData.Replace("Oyuncunun_Parasi : ", "");

            para = int.Parse(moneyString);

        }
    }

    //Oyun kapanýnca bütün verileri kaydediyoruz.
    private void OnApplicationQuit()
    {
        saveData();
    }

    //Parayý dosaydan okuduk ve ui üzerindeki texte yazdýk.
    public void loadMoneyUI()
    {

        if (paraText != null)
        {

            paraText.text = para.ToString();

        }

    }

    // Oyuncuya para ekleme iþlemi
    public void AddMoney(int amount)
    {
        para += amount;
        loadMoneyUI();
        saveData();
    }
}