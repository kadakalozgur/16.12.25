using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Transform oyuncu;
    public float hareketHizi = 2f;
    public float farkEtmeMesafesi = 50f; 
    public float durmaMesafesi = 0.8f;
    private Animator animator;
    private Rigidbody2D rb;
    private enemyCombat savasScripti; 
    private bool yuruyorMu = false;
    private bool sagaBakiyor = true; 

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        savasScripti = GetComponent<enemyCombat>();

        if (oyuncu == null)
        {
            GameObject oyuncuObjesi = GameObject.FindGameObjectWithTag("Player");

            if (oyuncuObjesi != null)
                oyuncu = oyuncuObjesi.transform;

        }
    }

    void FixedUpdate()
    {

        if (oyuncu == null || (savasScripti != null && savasScripti.OlduMu))
        {
            rb.velocity = Vector2.zero;
            return;
        }

        float mesafe = Vector2.Distance(transform.position, oyuncu.position);

        if (mesafe < farkEtmeMesafesi)
        {
           
            Vector2 yon = (oyuncu.position - transform.position).normalized;

            if (yon.x < 0 && !sagaBakiyor)
                YonCevir();

            else if (yon.x > 0 && sagaBakiyor)

                YonCevir();

            if (mesafe > durmaMesafesi)
            {
                Vector2 yeniPozisyon = Vector2.MoveTowards(rb.position, oyuncu.position, hareketHizi * Time.fixedDeltaTime);
                rb.MovePosition(yeniPozisyon);

                if (!yuruyorMu)
                {
                    animator.SetTrigger("MoveTrigger");
                    yuruyorMu = true;
                }
            }

            else
            {
                
                rb.velocity = Vector2.zero;

                if (yuruyorMu)
                {
                    yuruyorMu = false;
                }

                if (savasScripti != null)
                {
                    savasScripti.Saldir(oyuncu);
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            yuruyorMu = false;
        }
    }

    void YonCevir()
    {

        sagaBakiyor = !sagaBakiyor;
        Vector3 olcek = transform.localScale;
        olcek.x *= -1;
        transform.localScale = olcek;

    }
}