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

    // 공격 쿨타임을 관리할 타이머
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

        // 원거리 공격 범위 (파란색)
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, SO.rangedAttackRange);

        // 추격 범위 (노란색)
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
        _fsm.Owner.Flip(_fsm.Player); // 플레이어 방향으로 즉시 뒤집기

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
        // 추격 애니메이션

        // 추격 상태에 진입하면 NavMeshAgent 활성화
        _sheetFSM.Agent.isStopped = false;
        _sheetFSM.Agent.speed = _sheetFSM.SO.moveSpeed;
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);
        float sqrDistance = _fsm.GetSqrDistanceToPlayer();

        // 공격범위에 들어오면 공격 상태로 전환
        if (sqrDistance <= _sheetFSM.SO.rangedAttackRange * _sheetFSM.SO.rangedAttackRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.RangedAttackState);
            return;
        }

        // 추격 범위를 벗어나면 대기 상태로 전환
        if (sqrDistance > _sheetFSM.SO.chaseRange * _sheetFSM.SO.chaseRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.IdleState);
            return;
        }

        // 플레이어의 위치를 목적지로 계속 설정하여 추적
        if (_fsm.Player != null)
        {
            _sheetFSM.Agent.SetDestination(_fsm.Player.position);
        }
    }

    public override void Exit()
    {
        // 추격 상태를 벗어날 때 Agent의 움직임을 멈춤
        if (_sheetFSM.Agent.isActiveAndEnabled)
        {
            _sheetFSM.Agent.isStopped = true;
            _sheetFSM.Agent.ResetPath(); // 경로 초기화
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
        // 공격 상태 진입 시 NavMeshAgent를 정지시켜 제자리에 멈추게 함
        if (_sheetFSM.Agent.isActiveAndEnabled)
        {
            _sheetFSM.Agent.isStopped = true;
            _sheetFSM.Agent.ResetPath(); // 경로 초기화
        }
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);
        float sqrDistance = _fsm.GetSqrDistanceToPlayer();

        // 공격 범위를 벗어나면 다시 추격 상태로 전환
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
