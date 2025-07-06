using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HormingProjectile : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 dir = (target.position - transform.position).normalized;
        rb.velocity = dir * speed;
    }
}
