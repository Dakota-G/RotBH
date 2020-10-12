using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]    
    private int HP;
    
    [SerializeField]    
    private int MaxHP;
    private float speed;

    private Transform target;

    public Transform Target { get => target; set => target = value; }
    public float Speed { get => speed; set => speed = value; }
    public Healthbar HPbar;

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
