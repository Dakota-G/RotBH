using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Bullet_Coll : MonoBehaviour
{
    public int damage = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, 1f);
        if(collision.gameObject.tag == "Player")
        {
            PC_Class enemy = collision.gameObject.GetComponent<PC_Class>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
