using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] protected float spawnRange = 9f;
    [SerializeField] protected float enemiesCount = 3f;
    protected float returnNumber = 3f;     //Variable which helps return the old amount of enemies 
    
    [SerializeField] protected GameObject enemyPrefabs;
    [SerializeField] protected GameObject powerupPrefabs;

    
    private Vector3 GenerateRandomPosition()
    {
        Vector3 randomPos = new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
        return randomPos;
    }


    void SpawnEnemyWave()
    {
        for (int i = 0; i < enemiesCount; ++i)
        {
            Instantiate(enemyPrefabs, GenerateRandomPosition(), enemyPrefabs.transform.rotation);
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave();
    }

    // Update is called once per frame
    void Update()
    {
        enemiesCount = FindObjectsOfType<Enemy>().Length;
        if(enemiesCount == 0)
        {
            returnNumber += 1;
            enemiesCount += returnNumber;
            SpawnEnemyWave();
            Instantiate(powerupPrefabs, GenerateRandomPosition(), powerupPrefabs.transform.rotation);
        }
    }
}
