using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] protected float spawnRange = 9f;
    [SerializeField] protected float enemiesCount = 3f;
    protected float returnEnemies = 3f;     //Variable which helps return the old amount of enemies 

    [SerializeField] protected GameObject[] enemyPrefabs;
    [SerializeField] protected GameObject powerupPrefabs;

    [SerializeField] protected LevelManager getLevelManager;

    
    private Vector3 GenerateRandomPosition()
    {
        Vector3 randomPos = new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
        return randomPos;
    }


    void GenerateEnemyEachWave()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        for (int i = 0; i < getLevelManager.gameLevel; ++i)
        {
            Instantiate(enemyPrefabs[enemyIndex], GenerateRandomPosition(), enemyPrefabs[enemyIndex].transform.rotation);
        }
    }


    void GetThroughWave()
    {
        enemiesCount = FindObjectsOfType<Enemy>().Length;
        if(enemiesCount == 0)
        {
            returnEnemies += 1;
            enemiesCount += returnEnemies;
            getLevelManager.gameLevel = returnEnemies;
            GenerateEnemyEachWave();
            Instantiate(powerupPrefabs, GenerateRandomPosition(), powerupPrefabs.transform.rotation);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateEnemyEachWave();        //First time create ememies
    }

    // Update is called once per frame
    void Update()
    {
        GetThroughWave();
    }
}
