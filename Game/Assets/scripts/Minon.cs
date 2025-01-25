using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Minion : MonoBehaviour
{
    public int enemyhealth=30;
    public float speed = 0.9f;  // Düþman gemisinin hýzýný belirler
    public float rotationSpeed = 3f;  // Dönme hýzý
    public GameObject projectilePrefab;  // Mermi prefab'ý
    public GameObject explosionPrefab;
    
    public Transform shootPoint;  // Merminin ateþ edileceði nokta

    private Transform player;  // Oyuncu (spaceship) referansý
    private Rigidbody2D rb;
    private Animator ani;
    

    private ScoreSystem scoreSystem;
    private void Start()
    {
        GameObject sceneManagerObj = GameObject.FindGameObjectWithTag("SceneManager");
        if (sceneManagerObj != null)
        {
            scoreSystem = sceneManagerObj.GetComponent<ScoreSystem>();
        }

    }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("SpaceShip").transform;  // Oyuncu gemisini bul
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SpaceShipScript.isAlive)
        {
            // Oyuncuya doðru hareket etme
            MoveTowardsPlayer();

            // Oyuncuya doðru ateþ etme
            FireAtPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Oyuncu ile düþman arasýndaki mesafeyi hesapla
        Vector2 direction = (player.position - transform.position).normalized;

        // Gemiyi hedefe doðru hareket ettirme
        rb.velocity = direction * speed;

        // Dönüþ açýsýný hesapla
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Eðer sprite'ýn "sað" yerine "yukarý" yönde çizilmiþse (top-down gemi gibi),
        // -90f ekleyerek düzeltme yapabilirsiniz:
        angle -= 270f;

        // Hedef rotasyonu oluþtur
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        // Yumuþak bir dönüþ için Slerp veya RotateTowards kullanabilirsiniz
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    void FireAtPlayer()
    {
        // Düþman gemisinin ateþ etme sýklýðýný belirlemek için
        if (Random.Range(0f, 1f) < 0.006f)  //  ihtimalle ateþ etme
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
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Bullet")
        {
            if (enemyhealth < 1)
            {
                //öl
                Die();
            }
            else
            {
                enemyhealth = enemyhealth - 10;
                ani.SetTrigger("redHit");
                scoreSystem.IncreaseScore(40);
               
            }
        }
        if(collision.tag == "Ultimate")
        {
            if (enemyhealth < 1)
            {
                //öl
                Die();
                
            }
            else
            {
                enemyhealth = enemyhealth - 30;
                ani.SetTrigger("redHit");
                scoreSystem.IncreaseScore(80);

            }
        }
    }
    public void Die()
    {
       
        
        SoundManager.Instance.PlaySFX(SoundManager.Instance.explosionSound);
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        
        Destroy(gameObject, 0.5f);
    }
   
}
