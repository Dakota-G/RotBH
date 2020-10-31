using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    namespace ECombat
    {
    public class Attack : MonoBehaviour
    {
        public static void Shoot(Transform attackTarget, float speed, Transform EFirePointR, Transform EFirePointL, GameObject BulletPrefab)
        {
            // Check to see if the PC is on the right or left side and fire from that firePoint
            // (keeps the bullets from hitting the slime)
            if((attackTarget.position.x - EFirePointR.position.x) > 0)
            {
                GameObject bullet = Instantiate(BulletPrefab, EFirePointR.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                Vector2 Aim = new Vector2(attackTarget.position.x - EFirePointR.transform.position.x, attackTarget.position.y - EFirePointR.transform.position.y);
                rb.AddForce(Aim * (speed + 1.0f), ForceMode2D.Impulse);
            }
            else
            {
                GameObject bullet = Instantiate(BulletPrefab, EFirePointL.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                Vector2 Aim = new Vector2(attackTarget.position.x - EFirePointL.transform.position.x, attackTarget.position.y - EFirePointL.position.y);
                rb.AddForce(Aim * (speed + 1.0f), ForceMode2D.Impulse);
            }
        }
    }
    }
}

