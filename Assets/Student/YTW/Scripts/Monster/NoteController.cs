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

        // 방향벡터 라디안 계산 후 각도로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // PlayerHealth 스크립트가 있다고 가정
            // other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Debug.Log("플레이어에게 음표 명중");
            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            Destroy(gameObject);
        }
    }
}
