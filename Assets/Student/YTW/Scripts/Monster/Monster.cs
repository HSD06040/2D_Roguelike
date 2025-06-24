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
            Animator.SetTrigger("Hit");
        }

        if (CurrentHealth <= 0)
        {
            Die();
        }
        
    }

    protected virtual void Die()
    {
        Debug.Log($"{name} 사망");
        Destroy(gameObject);
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
