using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;
using Items;

namespace Players
{
    
    public class PC_Class : MonoBehaviour
    {

        public int _hP;
        private int HP { get => _hP; set => _hP = value; }
        public int _maxHP = 100;
        private int MaxHP { get => _maxHP; set => _maxHP = value; }
        public int _mP = 10;
        private int MP { get => _mP; set => _mP = value; }
        private int _inventorySize = 5;
        public int InventorySize { get => _inventorySize; set => _inventorySize = value; }
        private float _speed = 3f;
        private float _moveSpeed;
        private bool _isAlive = true;
        public bool IsAlive { get => _isAlive; set => _isAlive = value; }
        public List<Potion> HP_Pots;
        public List<Potion> MP_Pots;
        public AudioSource BottleOpen;
        public Rigidbody2D RB;
        public Camera Cam;
        public Vector2 Movement;
        public Vector2 MousePos;
        public Transform firePoint;
        public GameObject bulletPrefab;
        public float bulletForce = 1f;

        void Move()
        {
            Movement.x = Input.GetAxisRaw("Horizontal");
            Movement.y = Input.GetAxisRaw("Vertical");
            MousePos = Cam.ScreenToWorldPoint(Input.mousePosition);

            RB.MovePosition(RB.position + Movement * _moveSpeed * Time.fixedDeltaTime);
            Vector2 LookDir = MousePos - RB.position;
            float Angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg - 90f;
            RB.rotation = Angle;
            Debug.Log("Move!");
        }
        
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
                if(HP_Pots.Count > 0)
                {
                    // Limit Potions to only heal to the MaxHP
                    if(_hP + HP_Pots[0].AmountHealed < _maxHP)
                    {
                        _hP += HP_Pots[0].AmountHealed;
                        HP_Pots.RemoveAt(0);
                        BottleOpen.Play();
                        Debug.Log($"Delicious! You now have {HP} HP!");
                    }
                    else if(_hP + HP_Pots[0].AmountHealed >= _maxHP)
                    {
                        _hP = _maxHP;
                        HP_Pots.RemoveAt(0);
                        BottleOpen.Play();
                        Debug.Log($"Max HP! You have {HP} HP!");
                    }
                }
                else
                {
                    Debug.Log("No potion for you!");
                }
            }
            else if(Input.GetButtonDown("Inv2"))
            {
                if(MP_Pots.Count > 0)
                {
                    _mP += MP_Pots[0].AmountHealed;
                    MP_Pots.RemoveAt(0);
                    BottleOpen.Play();
                    Debug.Log($"Delicious! You now have {MP} MP!");
                }
                else
                {
                    Debug.Log("No Potion for you!");
                }
            }
        }

        public GameObject HpPrefab;
        public GameObject MpPrefab;
        void ThrowAllPotions()
        {
                Random rand = new Random();
                foreach(Items.Potion potion in HP_Pots)
                {
                    HpPrefab.GetComponent<Items.Potion>().AmountHealed = potion.AmountHealed;
                    GameObject ThisPotion = Instantiate(HpPrefab, firePoint.position,firePoint.rotation);
                    Rigidbody2D rb = ThisPotion.GetComponent<Rigidbody2D>();
                    rb.AddForce(new Vector2(Random.Range(0,360),Random.Range(0,360)),ForceMode2D.Force);
                }
                HP_Pots.Clear();
                foreach(Items.Potion potion in MP_Pots)
                {
                    MpPrefab.GetComponent<Items.Potion>().AmountHealed = potion.AmountHealed;
                    GameObject ThisPotion = Instantiate(MpPrefab, firePoint.position,firePoint.rotation);
                    Rigidbody2D rb = ThisPotion.GetComponent<Rigidbody2D>();
                    rb.AddForce(new Vector2(Random.Range(0,360),Random.Range(0,360)),ForceMode2D.Force);
                }
                MP_Pots.Clear();
        }
        public void TakeDamage(int damage)
        {
            _hP -= damage;
            if(_hP <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Debug.Log("DEAD");
            ThrowAllPotions();
            Destroy(gameObject);
        }

        void Start()
        {
            _hP = _maxHP;
        }
        void Update()
        {
            Move();
            Drink_Potion();
            Shoot();
            _moveSpeed = Input.GetKey(KeyCode.LeftShift) ? _speed*2 : _speed;
        }
        void FixedUpdate()
        {
            Move();
        }
    }
}