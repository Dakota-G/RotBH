using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class RandomSpawn : MonoBehaviour
    {
        public GameObject[] enemies;
        public Vector3 enemySpawnPosition;
        public float enemySpawnTime = 5f;

        public GameObject[] potions;
        public Vector3 potionSpawnPosition;
        public Vector3 obstacleSpawnPosition;
        // Start is called before the first frame update
        void Start()
        {
            // InvokeRepeating will call the method every 6sec.
            InvokeRepeating("SpawnRandom", enemySpawnTime, enemySpawnTime);
            SpawnPotions();
        }

        // Update is called once per frame
        void Update()
        {

        }
        void SpawnRandom()
        {
            foreach(GameObject enemy in enemies)
            {
                enemySpawnPosition.x = Random.Range(-5,5);
                enemySpawnPosition.y = Random.Range(-5,5);
                enemySpawnPosition.z = 0;
                Instantiate(enemy, enemySpawnPosition, Quaternion.identity);
            }
        }

        public void SpawnObstacle(GameObject obstacle, float xPos, float yPos)
        {
            obstacleSpawnPosition.x = Random.Range(-xPos,xPos);
            obstacleSpawnPosition.y = Random.Range(-yPos,yPos);
            obstacleSpawnPosition.z = 0;
            Debug.Log($"I'm making an obstacle at: x = {obstacleSpawnPosition.x} y = {obstacleSpawnPosition.y}");
            GameObject spawnedObstacle = Instantiate(obstacle, obstacleSpawnPosition, Quaternion.identity);
            spawnedObstacle.tag = "Obstacle";
        }

        void SpawnPotions()
        {
            foreach(GameObject potion in potions)
            {
                potionSpawnPosition.x = Random.Range(-8,8);
                potionSpawnPosition.y = Random.Range(-8,8);
                potionSpawnPosition.z = 0;
                Instantiate(potion, potionSpawnPosition, Quaternion.identity);
            }
        }
    }
}