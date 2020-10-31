using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemies;
using Mechanics;
using Players;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {   
        private int _hP;
        public int HP { get => _hP; set => _hP = value; }  
        private int _maxHP;
        public int MaxHP { get => _maxHP; set => _maxHP = value; }
        public Healthbar HPbar;
        private int _touchDamage = 1;
        private float _elapsedT = 0f;

        // Random Move attributes
        public float LatestWanderChangeTime;
        public float MoveChangeTime;
        public bool Wandering;
        public Vector2 WanderDirection;
        public Vector2 MovementPerSecond;

        // Follow attributes
        private float _speed;
        private Transform _target;
        public Transform Target { get => _target; set => _target = value; }
        public float Speed { get => _speed; set => _speed = value; }

        // Shooting attributes
        public GameObject BulletPrefab;
        private float _bulletForce = .005f;
        public Transform EFirePointR;
        public Transform EFirePointL;
        private Transform _attackTarget;
        public Transform AttackTarget { get => _attackTarget; set => _attackTarget = value;}
        int Rando;

        public int XP;

        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                PC_Class player = collision.gameObject.GetComponent<PC_Class>();
                if(player != null)
                {
                    _elapsedT = 0f;
                    player.TakeDamage(_touchDamage);
                }
            }
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "Player")
            {
                PC_Class player = collision.gameObject.GetComponent<PC_Class>();
                if(player != null){
                    _elapsedT += Time.deltaTime;
                    if(_elapsedT >= 1f)
                    {
                        _elapsedT %= 1f;
                        player.TakeDamage(_touchDamage);
                    }
                }
            }
        }

        public void TakeDamage(int damage)
        {
            _hP -= damage;
            HPbar.SetHealth(_hP, _maxHP);
            if(_hP <= 0)
            {
                Die();
                
            }
        }

        void Die()
        {
            Destroy(gameObject);
        }

        void Start()
        {
            _maxHP = 5;
            _hP = _maxHP;
            HPbar.SetHealth(_hP, _maxHP);
            _speed = 0.5f;
            LatestWanderChangeTime = 0f;
            Wandering = true;
            MoveChangeTime = Random.Range(2f,4f);
            EMovement.InactiveMoveset.CalculateRandomMove(this);
            _elapsedT = 0;
            XP = 1;
        }

        void Update()
        {
            if(_target != null)
            {
                EMovement.ActiveMoveset.FollowTarget(this, _target);
                // Couldn't figure out how to get the slime to fire on a timer.. 
                // so I gave him a 2% chance to fire per frame (still a lot of firing)
                Rando = Random.Range(0, 100);
                if(_attackTarget != null & Rando < 2)
                {
                    ECombat.Attack.Shoot(this, _attackTarget);
                }
            }
            else
            {
                EMovement.InactiveMoveset.Wander(this);
            }
        }
    }
}