using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDropSpawner : MonoBehaviour
{
    public GameObject healthDropPrefab;  // Saðlýk objesinin prefab'ý
    public float spawnInterval = 6f;     // Saðlýk objelerinin düþme aralýðý (saniye)
    public float spawnRangeX = 8f;       // Saðlýk objelerinin saða ve sola ne kadar daðýlabileceði (X ekseninde)

    private void Start()
    {
        InvokeRepeating("SpawnHealthDrop", 0f, spawnInterval);  // Saðlýk objelerini belirli aralýklarla düþür
    }

    private void SpawnHealthDrop()
    {
        // X ekseninde rastgele bir pozisyon belirle
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector2 spawnPos = new Vector2(spawnPosX, 5.5f);

        // Saðlýk objesini spawn et
        Instantiate(healthDropPrefab, spawnPos, Quaternion.identity);
        //Destroy(gameObject,5f);
    }
}
