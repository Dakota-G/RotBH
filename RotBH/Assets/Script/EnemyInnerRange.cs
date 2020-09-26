using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInnerRange : MonoBehaviour
{
    // Start is called before the first frame update

    private Enemy parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");
        if(collision.tag == "Player")
        {
            parent.Speed = 2f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            parent.Speed = 1f;
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
