using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]    
    private int HP;
    
    [SerializeField]    
    private int MaxHP = 5;
    public int damage = 1;
    private float speed;
    private Transform target;
    public Transform Target { get => target; set => target = value; }
    public float Speed { get => speed; set => speed = value; }
    public Healthbar HPbar;
    float elapsedT = 0f;

    void Start()
    {
        HP = MaxHP;
        HPbar.SetHealth(HP, MaxHP);
    }

    private void FollowTarget()
    {
        if(target != null)
        {
            Debug.Log("Target found");
            transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
        }
    }

    protected void Update()
    {
        FollowTarget();
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
