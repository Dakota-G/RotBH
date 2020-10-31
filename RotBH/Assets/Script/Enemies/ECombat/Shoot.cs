using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    namespace ECombat
    {
        public class Attack : MonoBehaviour
        {
            public static void Shoot(Enemy enemy, Transform attackTarget)
            {
                // Check to see if the PC is on the right or left side and fire from that firePoint
                // (keeps the bullets from hitting the slime)
                if((attackTarget.position.x - enemy.transform.position.x) > 0)
                {
                    GameObject bullet = Instantiate(enemy.BulletPrefab, enemy.EFirePointR.transform.position, Quaternion.identity);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    Vector2 Aim = new Vector2(attackTarget.position.x - enemy.transform.position.x, attackTarget.position.y - enemy.transform.position.y);
                    rb.AddForce(Aim * (enemy.Speed + 2.0f), ForceMode2D.Impulse);
                }
                else
                {
                    GameObject bullet = Instantiate(enemy.BulletPrefab, enemy.EFirePointL.transform.position, Quaternion.identity);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    Vector2 Aim = new Vector2(attackTarget.position.x - enemy.transform.position.x, attackTarget.position.y - enemy.transform.position.y);
                    rb.AddForce(Aim * (enemy.Speed + 2.0f), ForceMode2D.Impulse);
                }
            }
        }
    }
}

