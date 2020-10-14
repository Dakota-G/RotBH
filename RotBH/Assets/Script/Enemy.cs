using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]    
    private int HP;
    
    [SerializeField]    
    private int MaxHP;
    public Healthbar HPbar;
    public int damage = 1;
    float elapsedT = 0f;

    // Random Move attributes
    private float latestWanderChangeTime;
    private float moveChangeTime;
    private bool wandering;
    private Vector2 wanderDirection;
    private Vector2 movementPerSecond;

    // Follow attributes
    private float speed;
    private Transform target;
    public Transform Target { get => target; set => target = value; }
    public float Speed { get => speed; set => speed = value; }

    // Shooting attributes
    public GameObject bulletPrefab;
    public float bulletForce = .005f;
    public Transform EFirePointR;
    public Transform EFirePointL;
    private Transform attackTarget;
    public Transform AttackTarget { get => attackTarget; set => attackTarget = value;}
    int Rando;

    void Start()
    {
        HP = MaxHP;
        HPbar.SetHealth(HP, MaxHP);
        speed = 0.5f;
        latestWanderChangeTime = 0f;
        wandering = true;
        moveChangeTime = Random.Range(2f,4f);
        calculateRandomMove();
        elapsedT = 0;
    }

    private void FollowTarget()
    {
        int direction = 1;
        if((float)HP/MaxHP <= 0.2)
        {
            direction = -1;
        }       
        transform.position = Vector2.MoveTowards(transform.position, target.position, direction * Speed * Time.deltaTime);
    }

    void calculateRandomMove(){
        wanderDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = wanderDirection * speed;
    }

    protected void Update()
    {
        if(target != null)
        {
            FollowTarget();
            // Couldn't figure out how to get the slime to fire on a timer.. 
            // so I gave him a 2% chance to fire per frame (still a lot of firing)
            Rando = Random.Range(0, 100);
            if(AttackTarget != null & Rando < 2)
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
        if (Time.time - latestWanderChangeTime > moveChangeTime && wandering)
        {
            speed = 0;
            wandering = false;
            latestWanderChangeTime = Time.time;
            //Time until enemy begins moving again
            moveChangeTime = Random.Range(1f,2f);
        }
        else if(Time.time - latestWanderChangeTime > moveChangeTime && !wandering)
        {
            speed = 0.5f;
            latestWanderChangeTime = Time.time;
            calculateRandomMove();
            wandering = true;
            //Time until enemy stops again
            moveChangeTime = Random.Range(2f,4f);
        }  
        if(speed > 0)
        {
            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), 
            transform.position.y + (movementPerSecond.y * Time.deltaTime));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PC_Class player = collision.gameObject.GetComponent<PC_Class>();
            if(player != null)
            {
                elapsedT = 0f;
                player.TakeDamage(damage);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PC_Class player = collision.gameObject.GetComponent<PC_Class>();
            if(player != null){
                elapsedT += Time.deltaTime;
                if(elapsedT >= 1f)
                {
                    elapsedT %= 1f;
                    player.TakeDamage(damage);
                }
            }
        }
    }

    public void Attack()
    {
        // Check to see if the PC is on the right or left side and fire from that firePoint
        // (keeps the bullets from hitting the slime)
        if((AttackTarget.position.x - transform.position.x) > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, EFirePointR.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 Aim = new Vector2(AttackTarget.position.x - transform.position.x, AttackTarget.position.y - transform.position.y);
            rb.AddForce(Aim * (Speed + 1.0f), ForceMode2D.Impulse);
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, EFirePointL.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Vector2 Aim = new Vector2(AttackTarget.position.x - transform.position.x, AttackTarget.position.y - transform.position.y);
            rb.AddForce(Aim * (Speed + 1.0f), ForceMode2D.Impulse);
        }
    }
    public void TakeDamage(int damage)
    {
        this.HP -= damage;
        HPbar.SetHealth(this.HP, this.MaxHP);
        if(this.HP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
