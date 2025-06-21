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
    public SheetMusic_RangedAttackState RangedAttackState { get; private set; }

    // ���� ��Ÿ���� ������ Ÿ�̸�
    public float lastAttackTime;

    protected override void Awake()
    {
        base.Awake();

        Agent = GetComponent<NavMeshAgent>();

        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        Agent.speed = SO.moveSpeed;
        Owner.SetStats(SO.health);

        IdleState = new SheetMusic_IdleState(this);
        ChaseState = new SheetMusic_ChaseState(this);
        RangedAttackState = new SheetMusic_RangedAttackState(this);

    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }


    private void OnDrawGizmosSelected()
    {
        if (SO == null) return;

        // ���Ÿ� ���� ���� (�Ķ���)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, SO.rangedAttackRange);

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
        float sqrDistance = _fsm.GetSqrDistanceToPlayer();

        // ���ݹ����� ������ ���� ���·� ��ȯ
        if (sqrDistance <= _sheetFSM.SO.rangedAttackRange * _sheetFSM.SO.rangedAttackRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.RangedAttackState);
            return;
        }

        // �߰� ������ ����� ��� ���·� ��ȯ
        if (sqrDistance > _sheetFSM.SO.chaseRange * _sheetFSM.SO.chaseRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.IdleState);
            return;
        }

        // �÷��̾��� ��ġ�� �������� ��� �����Ͽ� ����
        if (_fsm.Player != null)
        {
            _sheetFSM.Agent.SetDestination(_fsm.Player.position);
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

public class SheetMusic_RangedAttackState : BaseState
{
    private SheetMusicMonsterFSM _sheetFSM;

    public SheetMusic_RangedAttackState(SheetMusicMonsterFSM fsm) : base(fsm)
    {
        _sheetFSM = fsm;
    }
    public override void Enter()
    {
        // ���� ���� ���� �� NavMeshAgent�� �������� ���ڸ��� ���߰� ��
        if (_sheetFSM.Agent.isActiveAndEnabled)
        {
            _sheetFSM.Agent.isStopped = true;
            _sheetFSM.Agent.ResetPath(); // ��� �ʱ�ȭ
        }
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);
        float sqrDistance = _fsm.GetSqrDistanceToPlayer();

        // ���� ������ ����� �ٽ� �߰� ���·� ��ȯ
        if (sqrDistance > _sheetFSM.SO.rangedAttackRange * _sheetFSM.SO.rangedAttackRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.ChaseState);
            return;
        }

        if (Time.time >= _sheetFSM.lastAttackTime + _sheetFSM.SO.rangedAttackCooldown)
        {
            FireNote();
        }
    }

    private void FireNote()
    {
        _sheetFSM.lastAttackTime = Time.time;
        Vector2 direction = (_fsm.Player.position - _fsm.Owner.transform.position).normalized;

        GameObject note = Object.Instantiate(_sheetFSM.SO.notePrefab, _fsm.Owner.transform.position, Quaternion.identity);
        note.GetComponent<NoteController>().Initialize(direction, _sheetFSM.SO.noteSpeed, _sheetFSM.SO.attackPower);
    }
}
