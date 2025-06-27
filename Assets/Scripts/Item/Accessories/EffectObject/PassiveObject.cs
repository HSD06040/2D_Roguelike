using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveObject : MonoBehaviour
{
    private float damage;
    private float radius;

    public virtual void Init(float _damage, float _radius)
    {
        transform.localScale = new Vector2(transform.localScale.x * _radius, transform.localScale.y * _radius);
        damage = Manager.Data.PlayerStatus.curWeapon.WeaponData.AttackDamage * _damage;
        radius = _radius;
        Manager.Resources.Destroy(gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((1 << 6 & (1 << collision.gameObject.layer)) != 0)
        {
            collision.GetComponent<IDamagable>().TakeDamage(damage);
        }
    }    
}
