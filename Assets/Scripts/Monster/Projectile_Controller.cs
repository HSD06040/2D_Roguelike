using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    private Rigidbody2D _rb;
    private int damage;
    private Coroutine _returnToPoolCoroutine;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    public void Initialize(Vector2 direction, float speed, int damage, string tag)
    {
        this.damage = damage;

        _rb.velocity = direction.normalized * speed;
        transform.up = direction;

        if (_returnToPoolCoroutine != null) StopCoroutine(_returnToPoolCoroutine);
        _returnToPoolCoroutine = StartCoroutine(ReturnToPoolAfterTime(5f)); // 수명 5초로 변경
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagable damageable))
        {
            // 자기 자신을 발사한 몬스터를 공격하지 않도록 예외 처리
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                damageable.TakeDamage(damage);
                Debug.Log($"{other.name}에게 원거리 공격 명중. 데미지 : {damage}");
                Manager.Resources.Destroy(gameObject);
                return;
            }
        }

        // 몬스터나 플레이어가 아닌 벽 같은 곳에 닿았을 때
        if ((1 << 9 & (1 << other.gameObject.layer)) != 0 || (1 << 10 & (1 << other.gameObject.layer)) != 0)
        {
            Manager.Resources.Destroy(gameObject);
        }
    }

    private IEnumerator ReturnToPoolAfterTime(float delay)
    {
        yield return CoroutineUtile.GetDelay(delay);
        Manager.Resources.Destroy(gameObject);
    }
}
