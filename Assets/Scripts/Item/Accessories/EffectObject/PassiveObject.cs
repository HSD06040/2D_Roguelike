using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveObject : MonoBehaviour
{
    protected float damage;

    public virtual void Init(float _damage, float _radius)
    {
        transform.localScale = new Vector2(transform.localScale.x * _radius, transform.localScale.y * _radius);
        damage = Manager.Data.PlayerStatus.TotalDamage * _damage;
        Manager.Resources.Destroy(gameObject, 2);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if((1 << 6 & (1 << collision.gameObject.layer)) != 0)
        {
            collision.GetComponent<IDamagable>().TakeDamage(damage);
        }
    }    
}
