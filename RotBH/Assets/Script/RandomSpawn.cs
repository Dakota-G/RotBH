using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public bool spawn;
    public int spawnReset;
    public Vector3 spawnPosition;
    public float spawnTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        // spawn = true;
        // spawnReset = 0;
        InvokeRepeating("SpawnRandom", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        // if(spawn == true)
        // {
        //     SpawnRandom();
        // }
        // else
        // {
        //     spawnReset += 1;
        //     if(spawnReset >= 10000)
        //     {
        //         spawn = true;
        //     }
        // }
    }
    void SpawnRandom()
    {
        foreach(GameObject enemy in enemies)
        {
            spawnPosition.x = Random.Range(-5,5);
            spawnPosition.y = Random.Range(-5,5);
            spawnPosition.z = 0;
            Instantiate(enemy, spawnPosition, Quaternion.identity);
        }
        spawn = false;
        spawnReset = 0;
    }
}
