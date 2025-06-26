using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
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
        _rb.velocity = direction.normalized * speed;
        this.damage = damage;

        // ���⺤�� ���� ��� �� ������ ��ȯ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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
