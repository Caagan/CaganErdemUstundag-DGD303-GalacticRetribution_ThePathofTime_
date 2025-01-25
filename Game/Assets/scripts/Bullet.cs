using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update

   
    void Start()
    {
        // Merminin belirli bir süre sonra yok olmasýný saðla
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Eðer mermi bir objeye çarparsa yok olsun
        Destroy(gameObject);
    }

}

   

