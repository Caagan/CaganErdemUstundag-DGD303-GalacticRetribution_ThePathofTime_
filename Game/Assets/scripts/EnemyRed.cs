using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipScript : MonoBehaviour
{
    public float speed = 0.6f;  // Düþman gemisinin hýzýný belirler
    public float rotationSpeed = 3f;  // Dönme hýzý
    public GameObject projectilePrefab;  // Mermi prefab'ý
    public Transform shootPoint;  // Merminin ateþ edileceði nokta

    private Transform player;  // Oyuncu (spaceship) referansý
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("SpaceShip").transform;  // Oyuncu gemisini bul
    }

    // Update is called once per frame
    void Update()
    {
        // Oyuncuya doðru hareket etme
        MoveTowardsPlayer();

        // Oyuncuya doðru ateþ etme
        FireAtPlayer();
    }

    void MoveTowardsPlayer()
    {
        // Oyuncu ile düþman arasýndaki mesafeyi hesapla
        Vector2 direction = (player.position - transform.position).normalized;

        // Düþman gemisini oyuncuya doðru hareket ettir
        rb.velocity = direction * speed;

        // Düþman gemisinin oyuncuya doðru dönmesi için
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void FireAtPlayer()
    {
        // Düþman gemisinin ateþ etme sýklýðýný belirlemek için
        if (Random.Range(0f, 1f) < 0.01f)  // %1 ihtimalle ateþ etme
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Mermiyi oluþtur
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Merminin hareket etmesi için Rigidbody2D'yi al
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Oyuncu ile düþman arasýndaki yönü hesapla
        Vector2 direction = (player.position - shootPoint.position).normalized;

        // Mermiyi oyuncuya doðru hareket ettir
        if (rb != null)
        {
            // Mermiyi oyuncuya doðru yönlendiriyoruz
            rb.velocity = direction * 10f;  // Hýz 10f olarak ayarlanabilir
        }
    }
}
