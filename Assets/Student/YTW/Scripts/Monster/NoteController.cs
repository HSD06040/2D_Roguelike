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

        // 충돌 시 데미지를 주기 위해 임시로 데미지 정보를 저장할 수 있음
        // 이 예제에서는 간단하게 처리
        // this.damage = damage; 

        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // PlayerHealth 스크립트가 있다고 가정
            // other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Debug.Log("플레이어에게 음표 명중!");
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Monster"))
        {
            Destroy(gameObject);
        }
    }
}
