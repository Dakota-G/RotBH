using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics;

namespace Players
{
    public class ObstacleCount : MonoBehaviour
    {
        private PC_Class _parent;
        public int _obstacleCount;
        private RandomSpawn _spawner = new RandomSpawn();
        public int distanceToLook;
        public int numberOfObstacles;
        public GameObject[] obstacleList;
        void Start()
        {
            _parent = GetComponentInParent<PC_Class>();
            for(int i=0; i<10; i++)
            {
                float spawnXPos = _parent.transform.position.x + distanceToLook;
                float spawnYPos = _parent.transform.position.y + distanceToLook;
                GameObject myObstacle = obstacleList[Random.Range(0, obstacleList.Length)];
                _spawner.SpawnObstacle(myObstacle,spawnXPos, spawnYPos);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Debug.Log($"You hit a thing! That thing was a {collision.tag}");
            if(collision.tag == "Obstacle")
            {
                _obstacleCount++;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "Obstacle")
            {
                _obstacleCount--;
            }
            if(_obstacleCount < numberOfObstacles)
            {
                float spawnXPos = _parent.transform.position.x + distanceToLook;
                float spawnYPos = _parent.transform.position.y + distanceToLook;
                GameObject myObstacle = obstacleList[Random.Range(0, obstacleList.Length)];
                _spawner.SpawnObstacle(myObstacle,spawnXPos, spawnYPos);
            }
        }
        // Update is called once per frame
        void Update()
        {
        }
    }
    
}
