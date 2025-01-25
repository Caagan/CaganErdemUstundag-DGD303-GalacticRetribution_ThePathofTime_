using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Minion : MonoBehaviour
{
    public int enemyhealth=30;
    public float speed = 0.9f;  // D��man gemisinin h�z�n� belirler
    public float rotationSpeed = 3f;  // D�nme h�z�
    public GameObject projectilePrefab;  // Mermi prefab'�
    public GameObject explosionPrefab;
    
    public Transform shootPoint;  // Merminin ate� edilece�i nokta

    private Transform player;  // Oyuncu (spaceship) referans�
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
            // Oyuncuya do�ru hareket etme
            MoveTowardsPlayer();

            // Oyuncuya do�ru ate� etme
            FireAtPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        // Oyuncu ile d��man aras�ndaki mesafeyi hesapla
        Vector2 direction = (player.position - transform.position).normalized;

        // Gemiyi hedefe do�ru hareket ettirme
        rb.velocity = direction * speed;

        // D�n�� a��s�n� hesapla
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // E�er sprite'�n "sa�" yerine "yukar�" y�nde �izilmi�se (top-down gemi gibi),
        // -90f ekleyerek d�zeltme yapabilirsiniz:
        angle -= 270f;

        // Hedef rotasyonu olu�tur
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

        // Yumu�ak bir d�n�� i�in Slerp veya RotateTowards kullanabilirsiniz
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    void FireAtPlayer()
    {
        // D��man gemisinin ate� etme s�kl���n� belirlemek i�in
        if (Random.Range(0f, 1f) < 0.006f)  //  ihtimalle ate� etme
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Mermiyi olu�tur
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        // Merminin hareket etmesi i�in Rigidbody2D'yi al
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Oyuncu ile d��man aras�ndaki y�n� hesapla
        Vector2 direction = (player.position - shootPoint.position).normalized;

        // Mermiyi oyuncuya do�ru hareket ettir
        if (rb != null)
        {
            // Mermiyi oyuncuya do�ru y�nlendiriyoruz
            rb.velocity = direction * 10f;  // H�z 10f olarak ayarlanabilir
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Bullet")
        {
            if (enemyhealth < 1)
            {
                //�l
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
                //�l
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
