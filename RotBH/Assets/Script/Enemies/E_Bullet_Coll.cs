using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using Players;

namespace Enemies
{
    public class E_Bullet_Coll : MonoBehaviour
    {
        private int _damage = 1;

        void OnCollisionEnter2D(Collision2D collision)
        {
            Destroy(gameObject, 1f);
            if(collision.gameObject.tag == "Player")
            {
                Players.PC_Class enemy = collision.gameObject.GetComponent<PC_Class>();
                if(enemy != null)
                {
                    enemy.TakeDamage(_damage);
                }
                Destroy(gameObject);
            }
        }
    }
}