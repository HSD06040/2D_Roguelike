using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    public void Initialize(Vector2 direction, float speed, float damage)
    {
        _rb.velocity = direction.normalized * speed;

        // �浹 �� �������� �ֱ� ���� �ӽ÷� ������ ������ ������ �� ����
        // �� ���������� �����ϰ� ó��
        // this.damage = damage; 

        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // PlayerHealth ��ũ��Ʈ�� �ִٰ� ����
            // other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Debug.Log("�÷��̾�� ��ǥ ����");
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }
    }
}
