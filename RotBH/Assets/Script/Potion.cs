using UnityEngine;
using UnityEngine.Audio;

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
            Debug.Log(player.HP);
            switch (type.ToString())
            {
                case "Health":
                    player.hp_pots.Add(this);
                    break;
                case "Mana":
                    player.mp_pots.Add(this);
                    break;
            }
            Destroy(potion);
        }
        // If we want enemies to be able to take potions or items as well
        else if(collider.tag == "Enemy")
        {
            Debug.Log("That enemy stole your potion!");
            Destroy(potion);
        }
    }
}
