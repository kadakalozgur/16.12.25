using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerCombat : MonoBehaviour
{
    public GameObject youDiedScreen;
    public GameObject playerHealthBar;
    public GameObject altýnUI;
    public float maxCan = 1;
    public float mevcutCan;
    public Slider canBariSlider;
    public Color hasarRengi = Color.red;

    private Animator anim;
    private bool olduMu = false;  
    private Color orijinalRenk;
    private float hasarRenkSuresi = 0.3f;
    private SpriteRenderer[] parcalar;
    

    void Start()
    {
        Transform modelObjesi = transform.Find("model");

        if (modelObjesi != null)
            anim = modelObjesi.GetComponent<Animator>();

        else
            anim = GetComponentInChildren<Animator>();


        parcalar = GetComponentsInChildren<SpriteRenderer>();

        if (parcalar.Length > 0)
            orijinalRenk = parcalar[0].color;

        mevcutCan = maxCan;

        if (canBariSlider != null)
        {
            canBariSlider.minValue = 0f;
            canBariSlider.maxValue = 1f;
            canBariSlider.value = 1f;
        }
    }

    void Update()
    {
        if (canBariSlider != null && !olduMu)
        {
            mevcutCan = canBariSlider.value;
        }

        if (mevcutCan <= 0 && !olduMu)
        {
            Ol();
        }
    }

    public void TakeDamage(float hasar)
    {
        if (olduMu) 
            return;

        mevcutCan -= hasar;

        if (parcalar != null)
        {
            CancelInvoke("RengiSifirla");

            foreach (var parca in parcalar)
                parca.color = hasarRengi;

            Invoke("RengiSifirla", hasarRenkSuresi);
        }

        if (canBariSlider != null)
        {
            canBariSlider.value = mevcutCan;
        }

        if (mevcutCan <= 0)
            Ol();
    }

    void RengiSifirla()
    {
        if (parcalar != null)
        {
            foreach (var parca in parcalar)
                parca.color = orijinalRenk;

        }
    }

    void Ol()
    {
        if (olduMu)
            return;

        olduMu = true;

        Swordman hareketScripti = GetComponent<Swordman>();

        if (hareketScripti != null)
            hareketScripti.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.simulated = false;
        }

        if (anim != null)
            anim.Play("Die");

        StartCoroutine(olmeSuresi(0.62f));

    }
    IEnumerator olmeSuresi(float delay)
    {
        yield return new WaitForSeconds(delay);

        youDiedScreen.SetActive(true);
        altýnUI.SetActive(false);
        playerHealthBar.SetActive(false);

        Time.timeScale = 0f;
    }
}
