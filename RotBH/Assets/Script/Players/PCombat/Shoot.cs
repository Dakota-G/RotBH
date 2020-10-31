using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using Items;

namespace Players
{
    namespace PCombat
    {
        public class Attack : MonoBehaviour
        {
            public static float bulletForce = 10f;
            public static PC_Class Origin;
            public static void Shoot(Transform firePoint, GameObject bulletPrefab, PC_Class Player)
            {
                if(Input.GetButtonDown("Fire1"))
                {    
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                    Bullet_Coll bullet_specs = bullet.GetComponent<Bullet_Coll>();
                    bullet_specs.Origin = Player;
                }
            }
        }
    }
}


