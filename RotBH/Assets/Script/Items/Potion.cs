using UnityEngine;
using Items;
using Players;

namespace Items
{
    public class Potion : MonoBehaviour
    {
        public GameObject Pot;
        public enum PotionType{Health,Mana};
        public PotionType Type;
        public float AmountHealed = .25f;
        void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.tag == "Player")
            {
                Debug.Log("WORK");
                Players.PC_Class player = collider.GetComponent<Players.PC_Class>();
                switch (Type.ToString())
                {
                    case "Health":
                        if(player.HP_Pots.Count < player.InventorySize && player.IsAlive == true)
                        {
                            player.HP_Pots.Add(this);
                            Destroy(Pot);
                        }
                        break; 
                    case "Mana":
                        if(player.MP_Pots.Count < player.InventorySize && player.IsAlive == true)
                        {
                            player.MP_Pots.Add(this);
                            Destroy(Pot);
                        }
                        break;
                }
            }
            // If we want enemies to be able to take potions or items as well
            else if(collider.tag == "Enemy")
            {
                Debug.Log("That enemy stole your potion!");
                Destroy(Pot);
            }
        }
    }
}