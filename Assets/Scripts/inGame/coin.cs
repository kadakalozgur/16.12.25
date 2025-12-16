using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class coin : MonoBehaviour
{

    public TMP_Text coinText;
    public int toplam_coin;

    private void Start()
    {

        GameObject text = GameObject.FindGameObjectWithTag("coinText");

        if (text != null)
            coinText = text.GetComponent<TMP_Text>();

    }

    void Update()
    {
        
        transform.Rotate(0, 300f * Time.deltaTime,0 );

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            fileManagerMoney fileManagerMoney = FindObjectOfType<fileManagerMoney>();

            fileManagerMoney.AddMoney(25);

            Destroy(gameObject);
        }
    }

    void coin_text_guncelle()
    {
            coinText.text = toplam_coin.ToString();
    }
}
