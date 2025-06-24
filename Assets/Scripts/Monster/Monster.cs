using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterStatusController monsterStatusCon {  get; private set; }
    public StateMachine StateMachine { get; protected set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public int FacingDirection { get; protected set; } = 1;

    protected virtual void Awake()
    {
        monsterStatusCon = GetComponent<MonsterStatusController>();
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

    protected virtual void Die()
    {
        Debug.Log($"{name} »ç¸Á");
        Destroy(gameObject);
    }
}
