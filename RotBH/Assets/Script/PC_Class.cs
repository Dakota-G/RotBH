using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Class : MonoBehaviour
{

    public int HP = 10;
    public int MP = 10;
    public float speed = 5f;
    public class Inventory
    {
        public int hp_pots;
        public int mp_pots;
        public Inventory(int hps, int mps)
        {
            hp_pots = hps;
            mp_pots = mps;
        }
    }
    public Inventory myInventory = new Inventory(0, 0);

    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    void Move_Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    void Look_Update()
    {
         rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

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

    void Update()
    {
        Move_Update();
        Look_Update();
        Shoot();
    }

}
