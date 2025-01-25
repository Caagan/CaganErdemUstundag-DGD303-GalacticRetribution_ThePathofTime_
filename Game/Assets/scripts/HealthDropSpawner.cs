using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDropSpawner : MonoBehaviour
{
    public GameObject healthDropPrefab;  // Sa�l�k objesinin prefab'�
    public float spawnInterval = 6f;     // Sa�l�k objelerinin d��me aral��� (saniye)
    public float spawnRangeX = 8f;       // Sa�l�k objelerinin sa�a ve sola ne kadar da��labilece�i (X ekseninde)

    private void Start()
    {
        InvokeRepeating("SpawnHealthDrop", 0f, spawnInterval);  // Sa�l�k objelerini belirli aral�klarla d���r
    }

    private void SpawnHealthDrop()
    {
        // X ekseninde rastgele bir pozisyon belirle
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector2 spawnPos = new Vector2(spawnPosX, 5.5f);

        // Sa�l�k objesini spawn et
        Instantiate(healthDropPrefab, spawnPos, Quaternion.identity);
        //Destroy(gameObject,5f);
    }
}
