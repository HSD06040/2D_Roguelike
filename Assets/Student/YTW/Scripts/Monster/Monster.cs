using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; protected set; } = 100f;
    public float CurrentHealth { get; protected set; }

    public StateMachine StateMachine { get; protected set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public int FacingDirection { get; protected set; } = 1;

    public virtual void SetStats(float maxHealth)
    {
        MaxHealth = maxHealth;
        CurrentHealth = maxHealth;
    }

    protected virtual void Awake()
    {
        CurrentHealth = MaxHealth;
        Animator = GetComponent<Animator>();
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
}
