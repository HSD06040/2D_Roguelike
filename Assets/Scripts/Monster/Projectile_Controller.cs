using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    private Rigidbody2D _rb;
    private int damage;
    private Coroutine _returnToPoolCoroutine;
    private float _speed;
    private Transform _playerTransform;

    [Header("유도 발사체 설정")]
    [SerializeField] private bool _isHoming = false; 
    [SerializeField] private float _homingStrength = 5f;

    [Header("발사체 수명")]
    [SerializeField] public float delay = 3f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    public void Initialize(Vector2 direction, float speed, int damage, string tag)
    {
        this.damage = damage;
        _speed = speed;
        _rb.velocity = direction.normalized * _speed;

        if (direction.sqrMagnitude > 0) // Zero 벡터일 경우 transform.up 설정 시 오류 방지
        {
            transform.up = direction;
        }

        if (_returnToPoolCoroutine != null) StopCoroutine(_returnToPoolCoroutine);
        _returnToPoolCoroutine = StartCoroutine(ReturnToPoolAfterTime(delay)); 

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            _playerTransform = playerObject.transform;
        }
    }
    private void FixedUpdate() 
    {
        if (_isHoming && _playerTransform != null)
        {
            Vector2 targetDirection = (_playerTransform.position - transform.position).normalized;

            Vector2 currentVelocityDirection = _rb.velocity.normalized;
            Vector2 newDirection = Vector2.MoveTowards(currentVelocityDirection, targetDirection, _homingStrength * Time.fixedDeltaTime);
            _rb.velocity = newDirection * _speed;

            if (_rb.velocity.sqrMagnitude > 0)
            {
                transform.up = _rb.velocity.normalized;
            }
        }
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
        yield return Utile.GetDelay(delay);
        Manager.Resources.Destroy(gameObject);
    }
}
