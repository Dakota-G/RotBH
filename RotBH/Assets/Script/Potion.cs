using UnityEngine;

public class Potion : MonoBehaviour
{
    public GameObject potion;
    public enum PotionType{Health,Mana};
    public PotionType type;
    public int AmountHealed;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            PC_Class player = collider.GetComponent<PC_Class>();
            switch (type.ToString())
            {
                case "Health":
                    if(player.hp_pots.Count < player.InventorySize && player.IsAlive == true)
                    {
                        player.hp_pots.Add(this);
                        Destroy(potion);
                    }
                    break; 
                case "Mana":
                    if(player.mp_pots.Count < player.InventorySize && player.IsAlive == true)
                    {
                        player.mp_pots.Add(this);
                        Destroy(potion);
                    }
                    break;
            }
        }
        // If we want enemies to be able to take potions or items as well
        else if(collider.tag == "Enemy")
        {
            Debug.Log("That enemy stole your potion!");
            Destroy(potion);
        }
    }
}
