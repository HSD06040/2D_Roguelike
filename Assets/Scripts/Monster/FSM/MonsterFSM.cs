using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterFSM : MonoBehaviour, IAnimationAttackHandler
{
    public Monster Owner { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public Transform Player { get; private set; }
    public NavMeshAgent Agent { get; private set; }

    protected virtual void Awake()
    {
        Owner = GetComponent<Monster>();
        StateMachine = new StateMachine();
        Agent = GetComponent<NavMeshAgent>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            Player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트가 없습니다");
        }

        if (Agent != null)
        {
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;
            Agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        }
    }

    protected virtual void Update()
    {
        StateMachine.Update();
    }

    public float GetSqrDistanceToPlayer()
    {
        if (Player == null) return float.MaxValue;
        return (Player.position - transform.position).sqrMagnitude;
    }

    public virtual void AnimationAttackTrigger()
    {
        // 기본적으로는 아무것도 하지 않음.
        // 공격 애니메이션이 있는 몬스터 FSM만 이 메소드를 재정의하여 사용
    }

    public virtual void OnAttackAnimationFinished()
    {
        // 기본적으로는 아무것도 하지 않음.
        // 공격 애니메이션이 있는 몬스터 FSM만 이 메소드를 재정의하여 사용
    }
    public void DestroyMonster()
    {
        Destroy(gameObject);
    }
}

