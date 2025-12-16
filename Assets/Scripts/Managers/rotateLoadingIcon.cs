using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Bu script yükleme ekranýndaki iconu her saniye döndürmek içindir.

public class rotateLoadingIcon : MonoBehaviour
{

    private void Update()
    {

        transform.Rotate(0f, 0f, -200 * Time.unscaledDeltaTime);

    }

}
