using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SpaceShipScript : MonoBehaviour

{


    // I got help from ChatGPT for codes!!!!!!!!!
    private Camera _mainCam;
  
    Rigidbody2D rb;
    Animator ani;
    SpriteRenderer rndr;
    public float speed;
    public int health = 100;
    public int enemyRedHit=5;
    public int minionHit = 2;
    public bool partCollected=false;
    public static bool isAlive = true;
  

    public float rotationSpeed = 5f;  // Döndürme hýzý
    public GameObject projectilePrefab;  // Mermi prefab'ý
    public GameObject explosionPrefab;
    public GameObject ultiefektPrefab; //
    
    public Transform shootPoint;  // Merminin ateþ edileceði nokta
    public Image ultiImage; // Sprite'ý deðiþtireceðimiz Image objesi
    public Sprite ultiReadySprite;
    public Sprite ultinotReadySprite;


    public  Image progressbarui;
    public GameObject gameover;
    public GameObject parttext;

    public Text ultiText;

   


    public float maxChargeTime = 10f; // Ultimate için maksimum þarj süresi (saniye)
    private float currentChargeTime = 0f; // Þu anki þarj zamaný
    public bool isUltimateReady = false; // Ulti hazýr mý?

    public GameObject laserPrefab; // Lazer prefab'ý
    public Transform laserSpawnPoint;
    public Transform laserSpawnPoint2;// Lazerin çýkacaðý nokta

    


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani=GetComponent<Animator>();
        rndr=GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _mainCam=Camera.main;
       
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed,0));
        rb.AddForce(new Vector2(0,Input.GetAxis("Vertical") * speed));

        // Fare imlecinin pozisyonunu al
        Vector2 mousePosition = _mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Uzay gemisinin pozisyonunu al
        Vector2 direction = mousePosition - (Vector2)transform.position;

        // Yönü normalize et ve dön
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Ateþ etme iþlemi
        if (Input.GetMouseButtonDown(0))  // Sol fare tuþuna basýldýðýnda
        {
            Shoot();
        }

        // Zamanla ulti barýný doldur
        if (currentChargeTime < maxChargeTime)
        {
            currentChargeTime += Time.deltaTime; // Her frame'de bir miktar arttýr

            float percentage = Mathf.Lerp(0f, 100f, currentChargeTime /maxChargeTime); // 0 ile 100 arasýnda bir deðer hesapla
            ultiText.text = Mathf.FloorToInt(percentage).ToString() + "%";
        }
        else
        {
            isUltimateReady = true; // Bar %100 olunca ulti hazýr
            ultiImage.sprite = ultiReadySprite; //ulti ready resmi de hazýr
        }

        // Ultiyi kullanma
        if (isUltimateReady && Input.GetKeyDown(KeyCode.X)&&isAlive)
        {
            FireLaser(); // Lazer ateþle
            currentChargeTime = 0f; // Ulti kullanýldýðýnda barý sýfýrla
            isUltimateReady = false; // Ultiyi kullanýnca hazýr durumu sýfýrla
            ultiImage.sprite = ultinotReadySprite;//ulti hazýr degilresmi

        }
        if (!isAlive)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isAlive = true; 
                // Aktif olan sahneyi yeniden yükle
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        

    }




    void FireLaser()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.ultiSound);//ultisesi
        GameObject ultiAni=Instantiate(ultiefektPrefab, transform.position, transform.rotation);//ultiefekti
        Destroy(ultiAni, 0.5f);

        // Lazer ateþle
        GameObject laser1 = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
        GameObject laser2 = Instantiate(laserPrefab, laserSpawnPoint2.position, laserSpawnPoint2.rotation);

        // Merminin hareket etmesi için Rigidbody2D'yi al
        Rigidbody2D rb1 = laser1.GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = laser2.GetComponent<Rigidbody2D>();


        // Mermiyi ileriye doðru hareket ettir
        if (rb1 && rb2 != null)
        {
            // Mermiyi geminin baktýðý yönde hareket ettiriyoruz
            rb1.velocity = transform.up * 10f;
            rb2.velocity = transform.up * 10f; // Geminin "yukarý" yönü (forward yönü) ile hareket eder
        }
    }
    void Shoot()
    {
        if(isAlive) {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.fireSound);
        // Mermiyi oluþtur
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Merminin hareket etmesi için Rigidbody2D'yi al
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        // Mermiyi ileriye doðru hareket ettir
        if (rb != null)
        {
            // Mermiyi geminin baktýðý yönde hareket ettiriyoruz
            rb.velocity = transform.up * 10f;  // Geminin "yukarý" yönü (forward yönü) ile hareket eder
        }
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
                DecreaseHealth(enemyRedHit);
            }
        }
            if (collision.tag == "MinionBullet")
            {
                if (health < 1)
                {
                    //öl

                    Death();
                }
                else
                {
                    DecreaseHealth(minionHit);
                }

            }
            
            if (collision.tag == "EnemyRed")
            {
                if (health < 1)
                {
                    //öl

                    Death();
                }
                else
                {
                    DecreaseHealth(2);//dusmanlardokununcada can kaybetsin
                }

            
        }
        if (collision.tag == "partDrop")//parça toplandi
        {
            Debug.Log("parcatoplandi");
            partCollected = true;
            parttext.SetActive(false);
            health = 10000;
            SoundManager.Instance.PlaySFX(SoundManager.Instance.healthSound);

        }
    }
    public void Death()
    {
        if (isAlive) {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.explosionSound);
            health = 0;
            rndr.enabled = false;
            Instantiate(explosionPrefab, transform.position, transform.rotation);
            progressbarui.fillAmount = 0f;
            gameover.SetActive(true);
            isAlive = false;
            
                // Klavyeden herhangi bir tuþa basýldýðýnda
             
            
        }
       
    }
    public void IncreaseHealth(int amount)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.healthSound);
        health += amount;
        // Saðlýk sýnýrlarýný kontrol et (örneðin, 100'ün üzerine çýkmamasý için)
        health = Mathf.Min(health, 100);
        Debug.Log("Player Health: " + health);
        progressbarui.fillAmount = progressbarui.fillAmount + 0.01f*amount;

    }
    public void DecreaseHealth(int amount)
    {
        health -= amount;
        
        health = Mathf.Min(health, 100);
        Debug.Log("Player Health: " + health);
        progressbarui.fillAmount = progressbarui.fillAmount - 0.01f * amount;

        ani.SetTrigger("getHit");

       

    }
   

  
    
   
}

