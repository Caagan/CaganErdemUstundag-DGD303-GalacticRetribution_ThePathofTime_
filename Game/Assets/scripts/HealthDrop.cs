using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    // Start is called before the first frame update
    public int healthAmount = 20;  // Bu miktar, health objesinin oyuncunun canýný ne kadar artýracaðýný belirler

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer karakterle çarpýþma olursa ve karakterin tag'i "Player" ise
        if (other.CompareTag("SpaceShip"))
        {
            // Saðlýk miktarýný artýr
            other.GetComponent<SpaceShipScript>().IncreaseHealth(healthAmount);

            // Saðlýk objesini yok et
            Destroy(gameObject);
        }
    }
}
