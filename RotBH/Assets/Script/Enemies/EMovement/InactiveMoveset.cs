using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

namespace Enemies
{
    namespace EMovement
    {
        public class InactiveMoveset : MonoBehaviour
        {
        public static void CalculateRandomMove(Enemy enemy){
            enemy.WanderDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            enemy.MovementPerSecond = enemy.WanderDirection * enemy.Speed;
        }

        public static void Wander(Enemy enemy)
        {
            if (Time.time - enemy.LatestWanderChangeTime > enemy.MoveChangeTime && enemy.Wandering)
            {
                enemy.Speed = 0;
                enemy.Wandering = false;
                enemy.LatestWanderChangeTime = Time.time;
                //Time until enemy begins moving again
                enemy.MoveChangeTime = Random.Range(1f,2f);
            }
            else if(Time.time - enemy.LatestWanderChangeTime > enemy.MoveChangeTime && !enemy.Wandering)
            {
                enemy.Speed = 0.5f;
                enemy.LatestWanderChangeTime = Time.time;
                CalculateRandomMove(enemy);
                enemy.Wandering = true;
                //Time until enemy stops again
                enemy.MoveChangeTime = Random.Range(2f,4f);
            }  
            if(enemy.Speed > 0)
            {
                enemy.transform.position = new Vector2(enemy.transform.position.x + (enemy.MovementPerSecond.x * Time.deltaTime), 
                enemy.transform.position.y + (enemy.MovementPerSecond.y * Time.deltaTime));
            }
        }
        }
    }
}