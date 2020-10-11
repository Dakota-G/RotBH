using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Coll : MonoBehaviour
{

    public int damage = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, .5f);
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);

            }
            Destroy(gameObject);
        }
    }

}
