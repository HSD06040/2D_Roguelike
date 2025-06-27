using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    private Rigidbody2D _rb;
    private int damage;
    private string poolTag;
    private Coroutine _returnToPoolCoroutine;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    public void Initialize(Vector2 direction, float speed, int damage, string tag)
    {
        this.damage = damage;
        this.poolTag = tag; // �±� ����

        _rb.velocity = direction.normalized * speed;
        transform.up = direction;

        if (_returnToPoolCoroutine != null) StopCoroutine(_returnToPoolCoroutine);
        _returnToPoolCoroutine = StartCoroutine(ReturnToPoolAfterTime(5f)); // ���� 5�ʷ� ����
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable damageable))
        {
            // �ڱ� �ڽ��� �߻��� ���͸� �������� �ʵ��� ���� ó��
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                damageable.TakeDamage(damage);
                Debug.Log($"{other.name}���� ���Ÿ� ���� ����. ������ : {damage}");
                Destroy(gameObject);
                return;
            }
        }

        // ���ͳ� �÷��̾ �ƴ� �� ���� ���� ����� ��
        if (other.gameObject.layer != LayerMask.NameToLayer("Monster") && other.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            ObjectPooler.Instance.ReturnToPool(poolTag, gameObject);
        }
    }

    private IEnumerator ReturnToPoolAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        ObjectPooler.Instance.ReturnToPool(poolTag, gameObject);
    }
}
