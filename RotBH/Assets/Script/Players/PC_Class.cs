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
        public float Speed { get => _speed; set => _speed = value; }
        private bool _isAlive;
        public bool IsAlive { get => _isAlive; set => _isAlive = value;}
        public List<Potion> HP_Pots;
        public List<Potion> MP_Pots;
        public AudioSource BottleOpen;
        public Rigidbody2D RB;
        public Camera Cam;
        public Transform firePoint;
        public GameObject bulletPrefab;

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

        public void TakeDamage(int damage)
        {
            _hP -= damage;
            if(_hP <= 0)
            {
                PCombat.Death.Die(this);
            }
        }

        void Start()
        {
            _hP = _maxHP;
            _isAlive = true;
        }

        void Update()
        {
            PMovement.MouseFollow.Move(this);
            Drink_Potion();
            PCombat.Attack.Shoot(firePoint, bulletPrefab);
        }

        void FixedUpdate()
        {
        }
    }
}