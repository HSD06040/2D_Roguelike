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
        cols = Physics2D.OverlapCircleAll(transform.position, 30f, 1 << 6);

        Transform closest = null;
        float min = float.MaxValue;

        foreach (Collider2D col in cols)
        {
            float distance = Vector2.Distance(col.transform.position, transform.position);
            if (distance < min)
            {
                min = distance;
                closest = col.transform;
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
