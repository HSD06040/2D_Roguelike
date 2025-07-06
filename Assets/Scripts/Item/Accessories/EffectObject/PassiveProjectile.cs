using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PassiveProjectile : PassiveObject
{    
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;

    private Rigidbody2D rb;
    private Collider2D[] cols;
    private Transform target;

    private Vector2 start;
    private Vector3 dir;
    private bool isHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        start = transform.position;
    }

    public override void Init(float _damage, float _radius)
    {
        base.Init(_damage, _radius);

        target = FindClosestEnemy();

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }        
    }

    private void Update()
    {
        if (isHit) return;

        if(target == null)
        {
            rb.velocity = dir;
            return;
        }

        dir = (target.position - transform.position).normalized;
        rb.velocity = dir * speed;
        dir.x = 0;
        dir.y = 0;
        transform.right = dir;
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
            isHit = true;
            anim.SetTrigger("Hit");
        }
    }

    private void DestroyObject() => Destroy(gameObject);
}
