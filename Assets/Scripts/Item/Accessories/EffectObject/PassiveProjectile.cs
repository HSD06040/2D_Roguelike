using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveProjectile : PassiveObject
{
    private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Init(float _damage, float _radius)
    {
        base.Init(_damage, _radius);
        Vector3 dir = (FindCloseEnemy().position - transform.position).normalized;
        transform.right = dir;
        rb.velocity = dir * speed;
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
