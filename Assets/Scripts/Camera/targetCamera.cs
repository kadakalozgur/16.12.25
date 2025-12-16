using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//Bu script kemeranýn oyuncuyu takip etmesi içindir.
public class targetCamera : MonoBehaviour
{

    public Transform player;

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}
