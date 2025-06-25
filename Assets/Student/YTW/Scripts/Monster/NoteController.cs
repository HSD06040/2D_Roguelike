using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float damage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    public void Initialize(Vector2 direction, float speed, float damage)
    {
        _rb.velocity = direction.normalized * speed;
        this.damage = damage;

        // ���⺤�� ���� ��� �� ������ ��ȯ
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // PlayerHealth ��ũ��Ʈ�� �ִٰ� ����
            // other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Debug.Log("�÷��̾�� ��ǥ ����");
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Destroy(gameObject);
        }
    }
}
