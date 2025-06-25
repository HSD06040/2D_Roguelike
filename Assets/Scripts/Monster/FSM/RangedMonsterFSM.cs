using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class RangedMonsterFSM : MonsterFSM
{
    [field: SerializeField] public RangedMonsterSO SO { get; private set; }

    public Ranged_IdleState IdleState { get; private set; }
    public Ranged_RepositionState RepositionState { get; private set; }
    public Ranged_AttackState AttackState { get; private set; }
    public Ranged_DieState DieState { get; private set;}

    public float lastAttackTime; 

    protected override void Awake()
    {
        base.Awake();
        
        Agent.stoppingDistance = 0.1f;

        Agent.speed = SO.moveSpeed;
        Owner.SetStats(SO.health, SO.attackPower);

        IdleState = new Ranged_IdleState(this);
        RepositionState = new Ranged_RepositionState(this);
        AttackState = new Ranged_AttackState(this);
        DieState = new Ranged_DieState(this);
    }

    private void Start()
    {
        lastAttackTime = -SO.attackCooldown;
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        if(Owner.CurrentHealth <= 0 && !(StateMachine.CurrentState is Ranged_DieState))
        {
            StateMachine.ChangeState(DieState);
        }
    }

    public override void AnimationAttackTrigger()
    {
        (StateMachine.CurrentState as Ranged_AttackState)?.TriggerAttack();
    }

    public override void OnAttackAnimationFinished()
    {
        StateMachine.ChangeState(RepositionState);
    }

    public void FindAndSetNewWanderPosition()
    {
        if (Player == null) return;

        float halfWidth = SO.repositionBoxWidth / 2;
        float halfHeight = SO.repositionBoxHeight / 2;

        Vector3 randomPoint = Player.position + new Vector3(Random.Range(-halfWidth, halfWidth), Random.Range(-halfHeight, halfHeight), 0);

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
        {
            Agent.SetDestination(hit.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (SO == null) return;

        // �÷��̾� Ž�� ���� (������ ��)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SO.detectionRange);

        // ���Ͱ� ������ ���� (����� �簢��)
        if (Application.isPlaying && Player != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 boxCenter = Player.position;
            Vector3 boxSize = new Vector3(SO.repositionBoxWidth, SO.repositionBoxHeight, 0);
            Gizmos.DrawWireCube(boxCenter, boxSize);
        }
    }
}

public class Ranged_IdleState : BaseState
{
    private RangedMonsterFSM _rangedFSM;
    public Ranged_IdleState(RangedMonsterFSM fsm) : base(fsm) => _rangedFSM = fsm;

    public override void Enter()
    {
        _rangedFSM.Owner.Animator.SetBool("IsChasing", false);
    }

    public override void Update()
    {
        // �÷��̾ Ž�� ���� �ȿ� ���Դٸ�
        if (_rangedFSM.GetSqrDistanceToPlayer() <= _rangedFSM.SO.detectionRange * _rangedFSM.SO.detectionRange)
        {
            // ���� �ൿ(��ġ ����) ���·� ��ȯ
            _rangedFSM.StateMachine.ChangeState(_rangedFSM.RepositionState);
        }
    }
}

public class Ranged_RepositionState : BaseState
{
    private RangedMonsterFSM _rangedFSM;
    public Ranged_RepositionState(RangedMonsterFSM fsm) : base(fsm) => _rangedFSM = fsm;

    public override void Enter()
    {
        _rangedFSM.Agent.isStopped = false;
        _rangedFSM.Owner.Animator.SetBool("IsChasing", true);
        _rangedFSM.FindAndSetNewWanderPosition(); 
    }

    public override void Update()
    {
        _rangedFSM.Owner.Flip(_rangedFSM.Player);

        // �������� ���� �����߰� ���� ��Ÿ���� �� �Ǿ��ٸ� ���� ���·� ��ȯ
        if (!_rangedFSM.Agent.pathPending && _rangedFSM.Agent.remainingDistance <= _rangedFSM.Agent.stoppingDistance)
        {
            if (Time.time >= _rangedFSM.lastAttackTime + _rangedFSM.SO.attackCooldown)
            {
                _rangedFSM.StateMachine.ChangeState(_rangedFSM.AttackState);
            }
            else
            {
                _rangedFSM.FindAndSetNewWanderPosition();
            }
        }
    }
}
public class Ranged_AttackState : BaseState
{
    private RangedMonsterFSM _rangedFSM
        ;
    public Ranged_AttackState(RangedMonsterFSM fsm) : base(fsm) => _rangedFSM = fsm;

    public override void Enter()
    {
        _rangedFSM.Agent.isStopped = true;
        _rangedFSM.Agent.ResetPath();
        _rangedFSM.Owner.Flip(_rangedFSM.Player);
        _rangedFSM.Owner.Animator.SetTrigger("Attack");
        _rangedFSM.lastAttackTime = Time.time;
    }

    public void TriggerAttack()
    {
        if (_rangedFSM.SO.notePrefab == null || _rangedFSM.Player == null) return;

        GameObject note = UnityEngine.Object.Instantiate(_rangedFSM.SO.notePrefab, _rangedFSM.transform.position, Quaternion.identity);
        NoteController noteController = note.GetComponent<NoteController>();
        Vector2 direction = (_rangedFSM.Player.position - _rangedFSM.transform.position).normalized;
        noteController.Initialize(direction, _rangedFSM.SO.noteSpeed, _rangedFSM.SO.attackPower);
    }
}

public class Ranged_DieState : BaseState
{
    private RangedMonsterFSM _rangedFSM;
    public Ranged_DieState(RangedMonsterFSM fsm) : base(fsm) => _rangedFSM = fsm;

    public override void Enter()
    {
        _rangedFSM.Agent.isStopped = true;
        if (_rangedFSM.Owner.TryGetComponent<Collider2D>(out var collider))
        {
            collider.enabled = false;
        }

        _rangedFSM.Owner.Animator.SetTrigger("Die");

        // ���� ��� ����
    }
}
