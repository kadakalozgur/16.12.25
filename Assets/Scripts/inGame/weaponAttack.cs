using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponAttack : MonoBehaviour
{

    public int hasar_miktari = 20;

    private Animator anim;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (anim != null)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                return; 
            }

        }

        enemyCombat dusman = other.GetComponent<enemyCombat>();

        if (dusman != null)
        {
            
            if (!dusman.OlduMu)
            {
                dusman.HasarAl(hasar_miktari);
            }

        }
    }
}
