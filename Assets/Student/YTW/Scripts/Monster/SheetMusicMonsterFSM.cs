using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheetMusicMonsterFSM : MonsterFSM
{
    [field: SerializeField] public SheetMusicMonsterSO SO { get; private set; }

    public NavMeshAgent Agent { get; private set; }
    public SheetMusic_IdleState IdleState { get; private set; }
    public SheetMusic_ChaseState ChaseState { get; private set; }
    public SheetMusic_MeleeAttackState MeleeAttackState { get; private set; }

    // ���� ��Ÿ���� ������ Ÿ�̸�
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


    private void OnDrawGizmosSelected()
    {
        if (SO == null) return;

        // ���Ÿ� ���� ���� (������)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SO.meleeAttackRange);

        // �߰� ���� (�����)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SO.chaseRange);

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
        _fsm.Owner.Flip(_fsm.Player); // �÷��̾� �������� ��� ������

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
        // �߰� �ִϸ��̼�

        // �߰� ���¿� �����ϸ� NavMeshAgent Ȱ��ȭ
        _sheetFSM.Agent.isStopped = false;
        _sheetFSM.Agent.speed = _sheetFSM.SO.moveSpeed;
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);

        // �÷��̾��� ��ġ�� �������� ��� �����Ͽ� ����
        if (_fsm.Player != null)
        {
            _sheetFSM.Agent.SetDestination(_fsm.Player.position);
        }

        if (!_sheetFSM.Agent.pathPending && _sheetFSM.Agent.remainingDistance <= _sheetFSM.Agent.stoppingDistance)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.MeleeAttackState);
            return;
        }

        // �߰� ������ ����� ��� ���·� ��ȯ (�� �κ��� �״�� �Ӵϴ�)
        float sqrDistance = _fsm.GetSqrDistanceToPlayer(); // ���� ���ǹ��� ������ �Ÿ��� �ٽ� ����ؾ� �մϴ�.
        if (sqrDistance > _sheetFSM.SO.chaseRange * _sheetFSM.SO.chaseRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.IdleState);
            return;
        }
    }

    public override void Exit()
    {
        // �߰� ���¸� ��� �� Agent�� �������� ����
        if (_sheetFSM.Agent.isActiveAndEnabled)
        {
            _sheetFSM.Agent.isStopped = true;
            _sheetFSM.Agent.ResetPath(); // ��� �ʱ�ȭ
        }
    }
}

public class SheetMusic_MeleeAttackState : BaseState
{
    private SheetMusicMonsterFSM _sheetFSM;

    public SheetMusic_MeleeAttackState(SheetMusicMonsterFSM fsm) : base(fsm)
    {
        _sheetFSM = fsm;
    }
    public override void Enter()
    {
        // ���� ���� ���� �� NavMeshAgent�� �������� ���ڸ��� ���߰� ��
        if (_sheetFSM.Agent.isActiveAndEnabled)
        {
            _sheetFSM.Agent.isStopped = true;
            _sheetFSM.Agent.ResetPath();
        }
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);
        float sqrDistance = _fsm.GetSqrDistanceToPlayer();

        // ���� ������ ����� �ٽ� �߰� ���·� ��ȯ
        if (sqrDistance > _sheetFSM.SO.meleeAttackRange * _sheetFSM.SO.meleeAttackRange) // meleeAttackRange�� ����
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.ChaseState);
            return;
        }

        // ��Ÿ���� �Ǹ� ���� ���� ����
        if (Time.time >= _sheetFSM.lastAttackTime + _sheetFSM.SO.meleeAttackCooldown) // meleeAttackCooldown���� ����
        {
            PerformMeleeAttack();
        }
    }

    private void PerformMeleeAttack()
    {
        _sheetFSM.lastAttackTime = Time.time;

        // TODO: ���⿡ ���� ���� ���� ������ �����ؾ� �մϴ�.
        // ���� �ִϸ��̼� ���, �÷��̾�� ������ ���� ��
        // PlayerController player = _fsm.Player.GetComponent<PlayerController>();
        // player.TakeDamage(_sheetFSM.SO.attackPower);

        Debug.Log("���� ����");
    }
}
