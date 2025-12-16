using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bu script karkaterin enrde spawn oclaðýný belirlemek içindir.
//Örneðin oyuncu evden çýktýðýdna evin kapýsýnýn önüde spawn olamlý köyün ortasýnda deðil.

public class spawnPlayer : MonoBehaviour
{

    public static string karakterinSonKonumu; //Diðer scriptlerden eriþirken hata almamak için static tanýmladýk.Yoksa hata veriyor.

    void Start()
    {

        if(karakterinSonKonumu == "outHouse")
        {

            transform.position = new Vector2(406, 203);

        }

        else if (karakterinSonKonumu == "outBar")
        {

            transform.position = new Vector2(405, 183);

        }

        else if (karakterinSonKonumu == "outCastle1")
        {

            transform.position = new Vector2(511, 212);

        }

        else if (karakterinSonKonumu == "outCastle2")
        {

            transform.position = new Vector2(484, 171);

        }

    }

}
