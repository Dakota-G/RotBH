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

    void Start()
    {
        HP = MaxHP;
        HPbar.SetHealth(HP, MaxHP);
        speed = 0.5f;
        latestWanderChangeTime = 0f;
        wandering = true;
        moveChangeTime = Random.Range(2f,4f);
        calculateRandomMove();
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
        }
        else
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
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        HPbar.SetHealth(HP, MaxHP);
        if(HP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);

    }
}
