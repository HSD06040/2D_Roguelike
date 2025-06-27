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
        _returnToPoolCoroutine = StartCoroutine(ReturnToPoolAfterTime(5f)); // ���� 5�ʷ� ����
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamagable damageable))
        {
            // �ڱ� �ڽ��� �߻��� ���͸� �������� �ʵ��� ���� ó��
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                damageable.TakeDamage(damage);
                Debug.Log($"{other.name}���� ���Ÿ� ���� ����. ������ : {damage}");
                Manager.Resources.Destroy(gameObject);
                return;
            }
        }

        // ���ͳ� �÷��̾ �ƴ� �� ���� ���� ����� ��
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
