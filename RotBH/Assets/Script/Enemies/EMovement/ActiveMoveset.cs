using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    namespace EMovement
    {
        public class ActiveMoveset : MonoBehaviour
        {
                public static void FollowTarget(Enemy enemy, Transform target)
                {
                    int direction = 1;
                    if((float)enemy.HP/enemy.MaxHP <= 0.2)
                    {
                        direction = -1;
                    }       
                    enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, target.position, direction * enemy.Speed * Time.deltaTime);
                }
        }
    }
}

