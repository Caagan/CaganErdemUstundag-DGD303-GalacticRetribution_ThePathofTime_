using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partdrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // E�er karakterle �arp��ma olursa ve karakterin tag'i "Player" ise
        if (other.CompareTag("SpaceShip"))
        {
           
            Destroy(gameObject);
        }
    }
}
