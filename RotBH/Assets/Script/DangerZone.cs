using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    private Enemy parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attaaa-AA-aaack!");
        if(collision.tag == "Player")
        {
            parent.AttackTarget = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            parent.AttackTarget = null;
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