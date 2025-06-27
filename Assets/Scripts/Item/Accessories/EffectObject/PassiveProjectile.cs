using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveProjectile : PassiveObject
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Init(float _damage, float _radius)
    {
        base.Init(_damage, _radius);

        Debug.Log(FindCloseEnemy());
        rb.velocity = (FindCloseEnemy().position - transform.position).normalized * speed;
    }

    private Transform FindCloseEnemy()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 30, 1 << 6);

        float minDistance = Mathf.Infinity;
        Transform result = null;

        foreach (Collider2D col in cols)
        {
            float distance = Vector2.Distance(transform.position, col.transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                result = col.transform;
            }
        }        

        return result;
    }
}
