using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    namespace PCMechanics
    {
        public class Death : MonoBehaviour
        {
            public static GameObject HpPrefab = (GameObject)Resources.Load("Items/HP_Pot");
            public static GameObject MpPrefab = (GameObject)Resources.Load("Items/MP_Pot");

            public static void Die(PC_Class PC)
            {
                Debug.Log("DEAD");
                ThrowAllPotions(PC);
                Destroy(PC.gameObject);
            }
            
            public static void ThrowAllPotions(PC_Class PC)
            {
                    Random rand = new Random();
                    foreach(Items.Potion potion in PC.HP_Pots)
                    {
                        HpPrefab.GetComponent<Items.Potion>().AmountHealed = potion.AmountHealed;
                        GameObject ThisPotion = Instantiate(HpPrefab, PC.firePoint.position, PC.firePoint.rotation);
                        Rigidbody2D rb = ThisPotion.GetComponent<Rigidbody2D>();
                        rb.AddForce(new Vector2(Random.Range(0,360),Random.Range(0,360)),ForceMode2D.Force);
                    }
                    PC.HP_Pots.Clear();
                    foreach(Items.Potion potion in PC.MP_Pots)
                    {
                        MpPrefab.GetComponent<Items.Potion>().AmountHealed = potion.AmountHealed;
                        GameObject ThisPotion = Instantiate(MpPrefab, PC.firePoint.position, PC.firePoint.rotation);
                        Rigidbody2D rb = ThisPotion.GetComponent<Rigidbody2D>();
                        rb.AddForce(new Vector2(Random.Range(0,360),Random.Range(0,360)),ForceMode2D.Force);
                    }
                    PC.MP_Pots.Clear();
            }
        }
    }
}
