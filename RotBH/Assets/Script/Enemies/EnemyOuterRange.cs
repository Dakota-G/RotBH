using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;

namespace Enemies
{
    public class EnemyOuterRange : MonoBehaviour
    {
        // Start is called before the first frame update

        private Enemy _parent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                _parent.Target = collision.transform;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                _parent.Speed = 0;
                _parent.Target = null;
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