using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    public GameObject meteorPrefab;  // Saðlýk objesinin prefab'ý
    public float spawnInterval = 20f;     // Saðlýk objelerinin düþme aralýðý (saniye)
    public float spawnRangeX = 8f;       // Saðlýk objelerinin saða ve sola ne kadar daðýlabileceði (X ekseninde)

    private void Start()
    {
        InvokeRepeating("SpawnMeteorDrop", 0f, spawnInterval);  // Saðlýk objelerini belirli aralýklarla düþür
    }

    private void SpawnMeteorDrop()
    {
        // X ekseninde rastgele bir pozisyon belirle
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector2 spawnPos = new Vector2(spawnPosX, 10.5f);

        
        Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
    }
}
 
