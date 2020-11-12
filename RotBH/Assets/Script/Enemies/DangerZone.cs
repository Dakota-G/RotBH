using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

namespace Enemies
{
    public class DangerZone : MonoBehaviour
    {
        private Enemy _parent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Debug.Log("Attaaa-AA-aaack!");
            if(collision.tag == "Player")
            {
                _parent.AttackTarget = collision.transform;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                _parent.AttackTarget = null;
            }
        }
        
        void Start()
        {
            
            _parent = GetComponentInParent<Enemy>();
        }
        // Update is called once per frame
        void Update()
        {
            
        }
    }
}