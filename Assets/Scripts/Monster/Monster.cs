using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; protected set; } = 100f;
    [field: SerializeField] public float AttackPower { get; protected set; } = 10f;


    public float CurrentHealth { get; protected set; }

    public MonsterStatusController monsterStatusCon {  get; private set; }
    public StateMachine StateMachine { get; protected set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public int FacingDirection { get; protected set; }

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public virtual void SetStats(float maxHealth, float attackPower)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
        AttackPower = attackPower;

    }
 
    protected virtual void Awake()
    {
        CurrentHealth = MaxHealth;
        Animator = GetComponentInChildren<Animator>();
        monsterStatusCon = GetComponent<MonsterStatusController>();
        Rb = GetComponent<Rigidbody2D>();

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }


    public virtual void Flip(Transform target)
    {
        if (target == null) return;

        float xPosition = target.position.x;
        int direction = xPosition > transform.position.x ? 1 : -1;

        if (FacingDirection != direction)
        {
            FacingDirection = direction;
            transform.localScale = new Vector3(FacingDirection, 1, 1);
        }
    }

    public virtual void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"{name}�� ������ {damage} ����. ���� hp : {CurrentHealth}");
        if (CurrentHealth > 0)
        {
            if (CurrentHealth > 0)
            {
                StopCoroutine(HitFlashCoroutine());
                StartCoroutine(HitFlashCoroutine());
            }
        }
    }

    private IEnumerator HitFlashCoroutine()
    {
        // TODO : ���� ������ �׽�Ʈ ����
        // ������ ���������� ����
        spriteRenderer.color = Color.red;

        // 0.15�� ���� ��� (�����̴� �ð�)
        yield return new WaitForSeconds(0.15f);

        // ���� �������� ����
        spriteRenderer.color = originalColor;

        // ���⿡ ���߿� �̵��ӵ� ���� ���� �߰� ����
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ������Ʈ�� "Player" �±׸� ������ �ִ��� Ȯ��
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾��� ü�� ������Ʈ�� �����ͼ� ������ ó�� ����
            Debug.Log($"�÷��̾�� �����Ͽ� ������ {AttackPower} ���� ");
        }
    }
}
