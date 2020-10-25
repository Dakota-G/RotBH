using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

namespace Players
{
    public class InventoryUpgrade : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider){
            if(collider.tag == "Player")
            {
                PC_Class player = collider.GetComponent<PC_Class>();
                player.InventorySize ++;
                Destroy(gameObject);
            }
        }
    }
}