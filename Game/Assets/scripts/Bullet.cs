using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

   
    void Start()
    {
        // Merminin belirli bir s�re sonra yok olmas�n� sa�la
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // E�er mermi bir objeye �arparsa yok olsun
        Destroy(gameObject);
    }

}

   

