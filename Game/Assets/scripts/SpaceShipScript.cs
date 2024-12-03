using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipScript : MonoBehaviour

{
    Rigidbody2D rb;
    public float speed;
    public int health = 100;

    public float rotationSpeed = 5f;  // Döndürme hýzý
    public GameObject projectilePrefab;  // Mermi prefab'ý
    public Transform shootPoint;  // Merminin ateþ edileceði nokta

    
    public  Image progressbarui;
    public GameObject gameover;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed,0));
        rb.AddForce(new Vector2(0,Input.GetAxis("Vertical") * speed));

        // Fare imlecinin pozisyonunu al
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Uzay gemisinin pozisyonunu al
        Vector2 direction = mousePosition - (Vector2)transform.position;

        // Yönü normalize et ve dön
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Ateþ etme iþlemi
        if (Input.GetMouseButtonDown(0))  // Sol fare tuþuna basýldýðýnda
        {
            Shoot();
        }

    }
    void Shoot()
    {
        // Mermiyi oluþtur
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, transform.rotation);

        // Merminin hareket etmesi için Rigidbody2D'yi al
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Mermiyi ileriye doðru hareket ettir
        if (rb != null)
        {
            // Mermiyi geminin baktýðý yönde hareket ettiriyoruz
            rb.velocity = transform.up * 10f;  // Geminin "yukarý" yönü (forward yönü) ile hareket eder
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyRedBullet")
        {
            if (health < 1)
            {
                //öl
                Death();
            }
            else
            {
                health = health - 5;
                progressbarui.fillAmount =progressbarui.fillAmount-0.05f;
            }
        }
    }
    public void Death()
    {
        gameover.SetActive(true);
    }
}

