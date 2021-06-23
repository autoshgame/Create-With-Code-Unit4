using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float spawnRange = 9f;

    [SerializeField] protected GameObject enemyPrefabs;

    //Get random position for player
    private Vector3 GenerateRandomPosition()
    {
        Vector3 randomPos = new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
        return randomPos;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Instantiate(enemyPrefabs, GenerateRandomPosition(), enemyPrefabs.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
