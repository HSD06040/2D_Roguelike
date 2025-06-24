using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; protected set; } = 100f;
    [field: SerializeField] public float AttackPower { get; protected set; } = 10f; 
    public float CurrentHealth { get; protected set; }

    public StateMachine StateMachine { get; protected set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public int FacingDirection { get; protected set; } = 1;

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
        Debug.Log($"{name}가 데미지 {damage} 받음. 현재 hp : {CurrentHealth}");
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
        // TODO : 아직 데미지 테스트 못함
        // 색상을 빨간색으로 변경
        spriteRenderer.color = Color.red;

        // 0.15초 동안 대기 (깜빡이는 시간)
        yield return new WaitForSeconds(0.15f);

        // 원래 색상으로 복구
        spriteRenderer.color = originalColor;

        // 여기에 나중에 이동속도 감소 로직 추가 가능
    }


    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어의 체력 컴포넌트를 가져와서 데미지 처리 예정
            Debug.Log($"플레이어와 접촉하여 데미지 {AttackPower} 가함 ");
        }
    }
}
