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
        for (int i = 0; i < 10; i++) // 최대 10번 시도하여 유효한 위치를 찾음
        {
            float randomAngle = Random.Range(0, 2 * Mathf.PI);
            float randomDistance = Random.Range(SO.minRepositionDistance, SO.maxRepositionDistance);

            Vector2 offset = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * randomDistance;
            Vector3 randomPoint = Player.position + (Vector3)offset;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas))
            {
                Agent.SetDestination(hit.position);
                return; // 유효한 위치를 찾았으면 함수 종료
            }
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
            Gizmos.DrawWireSphere(Player.position, SO.minRepositionDistance);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(Player.position, SO.maxRepositionDistance);
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
