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
            // �ڱ� �ڽ��� �߻��� ���͸� �������� �ʵ��� ���� ó��
            if (other.gameObject.layer != LayerMask.NameToLayer("Monster"))
            {
                damageable.TakeDamage(damage);
                Debug.Log($"{other.name}���� ���Ÿ� ���� ����. ������ : {damage}");
            }
        }

        // ���Ͱ� �ƴ� ���̳� �ٸ� ������Ʈ�� ��Ƶ� �ı�
        if (other.gameObject.layer != LayerMask.NameToLayer("Monster"))
        {
            Destroy(gameObject);
        }
    }
}
