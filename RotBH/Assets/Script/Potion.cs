using UnityEngine;
using UnityEngine.Audio;

public class Potion : MonoBehaviour
{
    public GameObject potion;
    public enum PotionType{Health,Mana};
    public PotionType type;
    public AudioSource bottleOpen;
    public int AmountHealed;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            Debug.Log("Drink me!");
            // // When we know how the player's HP is stored:
            // Player player = collisionInfo.collider
            switch (type.ToString())
            {
                case "Health":
                    Debug.Log("Health Potion");
                    bottleOpen.Play();
                   // player.health += AmountHealed;
                    break;
                case "Mana":
                    Debug.Log("Mana Potion");
                    bottleOpen.Play();
                    // player.mana += AmountHealed;
                    break;
            }
            Destroy(potion,.7f);
        }
        // If we want enemies to be able to take potions or items as well
        else if(collider.tag == "Enemy")
        {
            Debug.Log("That enemy stole your potion!");
            Destroy(potion);
        }
    }
}
