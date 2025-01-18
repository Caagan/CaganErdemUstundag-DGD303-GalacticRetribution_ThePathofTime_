using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    // Start is called before the first frame update
     // Bu miktar, health objesinin oyuncunun canýný ne kadar artýracaðýný belirler

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer karakterle çarpýþma olursa ve karakterin tag'i "Player" ise
        if (other.CompareTag("SpaceShip"))
        {
            // Saðlýk miktarýný artýr
            other.GetComponent<SpaceShipScript>().Death();

           
            Destroy(gameObject);
        }
    }
}
