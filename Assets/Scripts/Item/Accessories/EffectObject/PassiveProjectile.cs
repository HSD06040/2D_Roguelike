using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveProjectile : PassiveObject
{
    private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;
    private Collider2D[] cols;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Init(float _damage, float _radius)
    {
        base.Init(_damage, _radius);

        Transform target = FindClosestEnemy();
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = (target.position - transform.position).normalized;
        transform.right = dir;
        rb.velocity = dir * speed;
    }

    private Transform FindClosestEnemy()
    {
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, 30f, cols, 1 << 6);
        if (count == 0) return null;

        Transform closest = null;
        float minDistSq = float.MaxValue;
        Vector2 selfPos = transform.position;

        for (int i = 0; i < count; i++)
        {
            float distSq = ((Vector2)cols[i].transform.position - selfPos).sqrMagnitude;
            if (distSq < minDistSq)
            {
                minDistSq = distSq;
                closest = cols[i].transform;
            }
        }

        return closest;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((1 << 6 & (1 << collision.gameObject.layer)) != 0)
        {
            collision.GetComponent<IDamagable>().TakeDamage(damage);

            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            anim.SetTrigger("Hit");
        }
    }

    private void DestroyObject() => Destroy(gameObject);
}
