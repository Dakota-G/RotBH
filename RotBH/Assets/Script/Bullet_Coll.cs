using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Coll : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, .5f);
    }

}
