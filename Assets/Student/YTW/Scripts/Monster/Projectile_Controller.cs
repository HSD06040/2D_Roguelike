using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    private Rigidbody2D _rb;
    private int damage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    public void Initialize(Vector2 direction, float speed, int damage)
    {
        this.damage = damage;
        _rb.velocity = direction.normalized * speed;
        transform.up = direction;

        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable damageable))
        {
            // 자기 자신을 발사한 몬스터를 공격하지 않도록 예외 처리
            if (other.gameObject.layer != LayerMask.NameToLayer("Monster"))
            {
                damageable.TakeDamage(damage);
                Debug.Log($"{other.name}에게 원거리 공격 명중. 데미지 : {damage}");
            }
        }

        // 몬스터가 아닌 벽이나 다른 오브젝트에 닿아도 파괴
        if (other.gameObject.layer != LayerMask.NameToLayer("Monster"))
        {
            Destroy(gameObject);
        }
    }
}
