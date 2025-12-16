using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bu kýsýmda karkaterin objelerin arkasýndan geçtiðinde gözükmemesi içindir.

public class ObjectPosition : MonoBehaviour
{

    void Update()
    {
        float y = transform.position.y;

        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        int siralama = (int)(10000 - y * 5);

        //Sprites dizisi içindeki her objenin spriteýndaki sortinorder deðerini siralama deðerine eþitledik.

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.sortingOrder = siralama;
        }
    }

}
