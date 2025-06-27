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
            Debug.LogError("Player �±׸� ���� ������Ʈ�� �����ϴ�");
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
        // �⺻�����δ� �ƹ��͵� ���� ����.
        // ���� �ִϸ��̼��� �ִ� ���� FSM�� �� �޼ҵ带 �������Ͽ� ���
    }

    public virtual void OnAttackAnimationFinished()
    {
        // �⺻�����δ� �ƹ��͵� ���� ����.
        // ���� �ִϸ��̼��� �ִ� ���� FSM�� �� �޼ҵ带 �������Ͽ� ���
    }
    public void DestroyMonster()
    {
        Destroy(gameObject);
    }
}

