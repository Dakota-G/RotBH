using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOuterRange : MonoBehaviour
{
    // Start is called before the first frame update

    private Enemy parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            parent.Target = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            parent.Speed = 0;
            parent.Target = null;
        }
    }

    
    void Start()
    {
        parent = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
