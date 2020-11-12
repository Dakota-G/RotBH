using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

namespace Enemies
{
    public class EnemyInnerRange : MonoBehaviour
    {
        // Start is called before the first frame update

        private Enemy _parent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Debug.Log("Hit");
            if(collision.tag == "Player")
            {
                _parent.Speed = 2f;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                _parent.Speed = 1f;
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