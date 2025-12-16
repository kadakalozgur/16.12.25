using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCombat : MonoBehaviour
{
    public float saldiriHasari = 0.1f;
    public float saldiriBeklemeSuresi = 0.5f;
    public float saldiriMenzili = 1f;
    private float vurusGecikmesi = 0.6f;
    public int maxCan = 100;
    public float yokOlmaSuresi = 0.5f;
    public Color hasarRengi = Color.red;
    public float hasarRenkSuresi = 0.15f;
    private int mevcutCan;
    private float sonrakiSaldiriZamani = 0f;
    private bool olduMu = false;
    private Animator animator;
    private SpriteRenderer[] parcalar;
    private Color orijinalRenk;
    public GameObject coin;

    public bool OlduMu { get { return olduMu; } }

    void Start()
    {
        animator = GetComponent<Animator>();

        mevcutCan = maxCan;

        parcalar = GetComponentsInChildren<SpriteRenderer>();

        if (parcalar.Length > 0)
            orijinalRenk = parcalar[0].color;
    }

    public void Saldir(Transform hedefOyuncu)
    {
        if (olduMu) 
            return;

        if (Time.time >= sonrakiSaldiriZamani)
        {

            animator.SetTrigger("AttackTrigger");

            StartCoroutine(HasarVerGecikmeli(hedefOyuncu));

            sonrakiSaldiriZamani = Time.time + saldiriBeklemeSuresi;

        }
    }

    IEnumerator HasarVerGecikmeli(Transform hedef)
    {
        yield return new WaitForSeconds(vurusGecikmesi);

        if (!olduMu && hedef != null)
        {

            float mesafe = Vector2.Distance(transform.position, hedef.position);

            if (mesafe <= saldiriMenzili + 0.5f)
            {

                playerCombat oyuncuScripti = hedef.GetComponent<playerCombat>();


                if (oyuncuScripti != null)
                {
                    oyuncuScripti.TakeDamage(saldiriHasari);
                }
            }
        }
    }

    public void HasarAl(int gelenHasar)
    {
        if (olduMu) return;

        mevcutCan -= gelenHasar;

        if (parcalar != null)
        {
            CancelInvoke("RengiSifirla");

            foreach (var parca in parcalar) 
                parca.color = hasarRengi;

            Invoke("RengiSifirla", hasarRenkSuresi);
        }

        if (mevcutCan <= 0)
        {
            Ol();
        }
    }

    void RengiSifirla()
    {
        if (parcalar != null)
            foreach (var parca in parcalar) parca.color = orijinalRenk;
    }

    void Ol()
    {

        olduMu = true;
        animator.SetTrigger("DeathTrigger");

        float coin_dusme_orani = Random.value;

        if(coin_dusme_orani <= 1f)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;

        Destroy(gameObject, yokOlmaSuresi);
    }
}