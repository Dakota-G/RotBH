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
        [SerializeField]    
        private int _hP;
        public int GetHP { get => _hP; set => _hP = value; }
        
        [SerializeField]    
        private int _maxHP;
        public int MaxHP { get => _maxHP; set => _maxHP = value; }
        public Healthbar HPbar;
        private int _touchDamage = 1;
        private float _elapsedT = 0f;

        // Random Move attributes
        private float _latestWanderChangeTime;
        private float _moveChangeTime;
        private bool _wandering;
        private Vector2 _wanderDirection;
        private Vector2 _movementPerSecond;

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

        void Start()
        {
            _hP = _maxHP;
            HPbar.SetHealth(_hP, _maxHP);
            _speed = 0.5f;
            _latestWanderChangeTime = 0f;
            _wandering = true;
            _moveChangeTime = Random.Range(2f,4f);
            CalculateRandomMove();
            _elapsedT = 0;
        }

        private void FollowTarget()
        {
            int direction = 1;
            if((float)_hP/_maxHP <= 0.2)
            {
                direction = -1;
            }       
            transform.position = Vector2.MoveTowards(transform.position, _target.position, direction * Speed * Time.deltaTime);
        }

        void CalculateRandomMove(){
            _wanderDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            _movementPerSecond = _wanderDirection * _speed;
        }

        protected void Update()
        {
            if(_target != null)
            {
                FollowTarget();
                // Couldn't figure out how to get the slime to fire on a timer.. 
                // so I gave him a 2% chance to fire per frame (still a lot of firing)
                Rando = Random.Range(0, 100);
                if(_attackTarget != null & Rando < 2)
                {
                    Attack();
                }
            }
            else
            {
                Wander();
            }
        }

        void Wander()
        {
            if (Time.time - _latestWanderChangeTime > _moveChangeTime && _wandering)
            {
                _speed = 0;
                _wandering = false;
                _latestWanderChangeTime = Time.time;
                //Time until enemy begins moving again
                _moveChangeTime = Random.Range(1f,2f);
            }
            else if(Time.time - _latestWanderChangeTime > _moveChangeTime && !_wandering)
            {
                _speed = 0.5f;
                _latestWanderChangeTime = Time.time;
                CalculateRandomMove();
                _wandering = true;
                //Time until enemy stops again
                _moveChangeTime = Random.Range(2f,4f);
            }  
            if(_speed > 0)
            {
                transform.position = new Vector2(transform.position.x + (_movementPerSecond.x * Time.deltaTime), 
                transform.position.y + (_movementPerSecond.y * Time.deltaTime));
            }
        }

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

        public void Attack()
        {
            // Check to see if the PC is on the right or left side and fire from that firePoint
            // (keeps the bullets from hitting the slime)
            if((_attackTarget.position.x - transform.position.x) > 0)
            {
                GameObject bullet = Instantiate(BulletPrefab, EFirePointR.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                Vector2 Aim = new Vector2(_attackTarget.position.x - transform.position.x, _attackTarget.position.y - transform.position.y);
                rb.AddForce(Aim * (_speed + 1.0f), ForceMode2D.Impulse);
            }
            else
            {
                GameObject bullet = Instantiate(BulletPrefab, EFirePointL.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                Vector2 Aim = new Vector2(_attackTarget.position.x - transform.position.x, _attackTarget.position.y - transform.position.y);
                rb.AddForce(Aim * (_speed + 1.0f), ForceMode2D.Impulse);
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
    }
}