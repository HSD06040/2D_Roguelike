using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class SheetMusicMonsterFSM : MonsterFSM
{
    [field: SerializeField] public SheetMusicMonsterSO SO { get; private set; }

    public NavMeshAgent Agent { get; private set; }
    public SheetMusic_IdleState IdleState { get; private set; }
    public SheetMusic_ChaseState ChaseState { get; private set; }
    public SheetMusic_MeleeAttackState MeleeAttackState { get; private set; } 
    public bool IsPlayerInAttackRange { get; private set; } = false;

    public float lastAttackTime;


    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();

        Agent.stoppingDistance = SO.meleeAttackRange;
        Agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        Agent.speed = SO.moveSpeed;
        Owner.SetStats(SO.health);

        IdleState = new SheetMusic_IdleState(this);
        ChaseState = new SheetMusic_ChaseState(this);
        MeleeAttackState = new SheetMusic_MeleeAttackState(this);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInAttackRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInAttackRange = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (SO == null) return;

        // �߰� ���� (����� ��)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SO.chaseRange);

        // ���� ���� (������ ��)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SO.meleeAttackRange);
    }
}

public class SheetMusic_IdleState : BaseState
{
    private SheetMusicMonsterFSM _sheetFSM;

    public SheetMusic_IdleState(SheetMusicMonsterFSM fsm) : base(fsm)
    {
        _sheetFSM = fsm;
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);

        float sqrDistance = _fsm.GetSqrDistanceToPlayer();
        if (sqrDistance <= _sheetFSM.SO.chaseRange * _sheetFSM.SO.chaseRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.ChaseState);
        }
    }
}

public class SheetMusic_ChaseState : BaseState
{
    private SheetMusicMonsterFSM _sheetFSM;

    public SheetMusic_ChaseState(SheetMusicMonsterFSM fsm) : base(fsm)
    {
        _sheetFSM = fsm;
    }

    public override void Enter()
    {
        _sheetFSM.Agent.isStopped = false; 
        _sheetFSM.Agent.speed = _sheetFSM.SO.moveSpeed;
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);

        if (_fsm.Player != null)
        {
            _sheetFSM.Agent.SetDestination(_fsm.Player.position);
        }

        //  �÷��̾ ���� ���� Ʈ���� �ȿ� ������ ���� ���·� ��ȯ
        if (_sheetFSM.IsPlayerInAttackRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.MeleeAttackState);
            return;
        }

        // �߰� ������ ����� ��� ���·� ��ȯ
        float sqrDistance = _fsm.GetSqrDistanceToPlayer();
        if (sqrDistance > _sheetFSM.SO.chaseRange * _sheetFSM.SO.chaseRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.IdleState);
        }
    }
}

public class SheetMusic_MeleeAttackState : BaseState
{
    private SheetMusicMonsterFSM _sheetFSM;
    private float _attackStartTime;

    public SheetMusic_MeleeAttackState(SheetMusicMonsterFSM fsm) : base(fsm)
    {
        _sheetFSM = fsm;
    }

    public override void Enter()
    {
        _sheetFSM.Agent.isStopped = true; // ���� �߿��� �̵� ����
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);

        // [����] �÷��̾ ���� ������ ����� ��� �ٽ� �߰� ���·� ��ȯ
        if (!_sheetFSM.IsPlayerInAttackRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.ChaseState);
            return;
        }

        // ���� ��Ÿ���� �� �Ǿ��ٸ� ���� ����
        if (Time.time >= _sheetFSM.lastAttackTime + _sheetFSM.SO.meleeAttackCooldown)
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        _sheetFSM.lastAttackTime = Time.time;

        // ���� �ִϸ��̼�, ����, ������ ó�� ����
        // ���� ����
        // 1. ���� �ִϸ��̼� ��� (Animator.SetTrigger("Attack"))
        // 2. ���� ȿ���� ��� (AudioSource.PlayOneShot(attackSound))
        // 3. �÷��̾�� ������ ����
        //  PlayerHealth playerHealth = _fsm.Player.GetComponent<PlayerHealth>();
        //    
        //  player.TakeDamage(_sheetFSM.SO.attackPower);
        Debug.Log("���� ����");
    }

    public override void Exit()
    {
        if (_sheetFSM.Agent.isActiveAndEnabled)
        {
            _sheetFSM.Agent.isStopped = false;
        }
    }
}