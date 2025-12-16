using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bu scripte rulet minigamenin içine girmek için yazýlmýþtýr.

public class joinRoulette : MonoBehaviour
{
    private SpriteRenderer rouletteSprite;
    private bool playerContactRouletteTable = false;

    public GameObject enterPlayButtonIcon;
    public GameObject rouletteTableUI;
    public GameObject moneyIconUI;

    void Start()
    {

        rouletteSprite = GetComponent<SpriteRenderer>();
        enterPlayButtonIcon.SetActive(false);
        rouletteTableUI.SetActive(false);

    }

    void Update()
    {

        if (playerContactRouletteTable)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {

                enterPlayButtonIcon.SetActive(false);
                rouletteTableUI.SetActive(true);
                moneyIconUI.SetActive(false);

                rouletteSprite.color = Color.white;            

                Swordman.canMove = false;

            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            playerContactRouletteTable = true;

            if(rouletteSprite != null)
            {

                rouletteSprite.color = new Color(0.67f, 0.75f, 0.0f);
                enterPlayButtonIcon.SetActive(true);

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            playerContactRouletteTable = false;

            if (rouletteSprite != null)
            {

                rouletteSprite.color = Color.white;
                enterPlayButtonIcon.SetActive(false);

            }
        }
    }
}
