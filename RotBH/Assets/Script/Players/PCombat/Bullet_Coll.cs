using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using Enemies;

namespace Players
{
    namespace PCombat
    {
        public class Bullet_Coll : MonoBehaviour
        {
            public PC_Class Origin;
            public int damage = 1;

            void OnCollisionEnter2D(Collision2D collision)
            {
                Destroy(gameObject, .5f);
                if(collision.gameObject.tag == "Enemy")
                {
                    Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                    if(enemy != null)
                    {
                        if (enemy.HP - damage <= 0)
                        {
                            Origin.Experience += enemy.XP;
                        }
                        enemy.TakeDamage(damage);
                    }
                    Destroy(gameObject);
                }
            }
        }
    }
}