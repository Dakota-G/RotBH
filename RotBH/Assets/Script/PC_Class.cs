using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Class : MonoBehaviour
{

    public int HP;
    public int MaxHP = 100;
    public int MP = 10;
    public int InventorySize = 5;
    public float speed = 3f;
    private float moveSpeed;
    public bool IsAlive = true;
    public List<Potion> hp_pots;
    public List<Potion> mp_pots;
    public AudioSource bottleOpen;
    // public class Inventory
    // {
        // public Inventory(int hps, int mps)
        // {
        //     hp_pots = hps;
        //     mp_pots = mps;
        // }
    // }
    // public Inventory myInventory = new Inventory();

    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;

    void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
    
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 1f;
    void Shoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {    
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }
    void Drink_Potion()
    {
        if(Input.GetButtonDown("Inv1"))
        {
            if(hp_pots.Count > 0)
            {
                // Limit Potions to only heal to the MaxHP
                if(this.HP + hp_pots[0].AmountHealed < this.MaxHP)
                {
                    this.HP += hp_pots[0].AmountHealed;
                    this.hp_pots.RemoveAt(0);
                    bottleOpen.Play();
                    Debug.Log($"Delicious! You now have {this.HP} HP!");
                }
                else if(this.HP + hp_pots[0].AmountHealed >= this.MaxHP)
                {
                    this.HP = this.MaxHP;
                    this.hp_pots.RemoveAt(0);
                    bottleOpen.Play();
                    Debug.Log($"Max HP! You have {this.HP} HP!");
                }
            }
            else
            {
                Debug.Log("No potion for you!");
            }
        }
        else if(Input.GetButtonDown("Inv2"))
        {
            if(mp_pots.Count > 0)
            {
                this.MP += mp_pots[0].AmountHealed;
                this.mp_pots.RemoveAt(0);
                bottleOpen.Play();
                Debug.Log($"Delicious! You now have {this.MP} MP!");
            }
            else
            {
                Debug.Log("No Potion for you!");
            }
        }
    }

    public GameObject hpPrefab;
    public GameObject mpPrefab;
    void ThrowAllPotions()
    {
            Random rand = new Random();
            foreach(Potion potion in hp_pots)
            {
                hpPrefab.GetComponent<Potion>().AmountHealed = potion.AmountHealed;
                GameObject ThisPotion = Instantiate(hpPrefab, firePoint.position,firePoint.rotation);
                Rigidbody2D rb = ThisPotion.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(Random.Range(0,360),Random.Range(0,360)),ForceMode2D.Force);
            }
            hp_pots.Clear();
            foreach(Potion potion in mp_pots)
            {
                mpPrefab.GetComponent<Potion>().AmountHealed = potion.AmountHealed;
                GameObject ThisPotion = Instantiate(mpPrefab, firePoint.position,firePoint.rotation);
                Rigidbody2D rb = ThisPotion.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(Random.Range(0,360),Random.Range(0,360)),ForceMode2D.Force);
            }
            mp_pots.Clear();
    }
    public void TakeDamage(int damage)
    {
        this.HP -= damage;
        if(HP <= 0)
        {
            this.Die();
        }
    }

    void Die()
    {
        ThrowAllPotions();
        Destroy(gameObject);
    }

    void Start()
    {
        this.HP = this.MaxHP;
    }
    void Update()
    {
        Drink_Potion();
        Shoot();
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? speed*2 : speed;
    }
    void FixedUpdate()
    {
        Move();
    }

}