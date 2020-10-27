using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Players
{
    namespace PCMechanics
    {
        public class Attack : MonoBehaviour
        {
            public static float bulletForce = 10f;
            public static void Shoot(Transform firePoint, GameObject bulletPrefab)
            {
                if(Input.GetButtonDown("Fire1"))
                {    
                    GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}


