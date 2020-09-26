using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Transform target;

    public Transform Target { get => target; set => target = value; }
    public float Speed { get => speed; set => speed = value; }

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
}
