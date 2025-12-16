using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bu script rulet oyunu bittiðinde okun hangi renkte durduðunu tespit etmemiz içindir.
public class stopArrow : MonoBehaviour
{

    public string color = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("red"))
        {
            color = "red";
        }

        else if (collision.CompareTag("black"))
        {
            color = "black";
        }

        else if (collision.CompareTag("green"))
        {
            color = "green";
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        OnTriggerEnter2D(collision); 

    }

}
