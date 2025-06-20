using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheetMusicMonsterFSM : MonsterFSM
{
    [field: SerializeField] public SheetMusicMonsterSO SO { get; private set; }

    public SheetMusic_IdleState IdleState { get; private set; }
    public SheetMusic_ChaseState ChaseState { get; private set; }
    public SheetMusic_RangedAttackState RangedAttackState { get; private set; }

    // 공격 쿨타임을 관리할 타이머
    public float lastAttackTime;

    protected override void Awake()
    {
        base.Awake();

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
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);
        float sqrDistance = _fsm.GetSqrDistanceToPlayer();

        if (sqrDistance <= _sheetFSM.SO.rangedAttackRange * _sheetFSM.SO.rangedAttackRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.RangedAttackState);
            return;
        }

        if (sqrDistance > _sheetFSM.SO.chaseRange * _sheetFSM.SO.chaseRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.IdleState);
            return;
        }

        // rangedAttackRange에 도달하기 전까지 계속 플레이어를 향해 이동
        Vector2 direction = (_fsm.Player.position - _fsm.Owner.transform.position).normalized;
        _fsm.Owner.Rb.velocity = direction * _sheetFSM.SO.moveSpeed;
    }

    public override void Exit()
    {
        _fsm.Owner.Rb.velocity = Vector2.zero; // 상태를 나갈 때 속도를 0으로 초기화
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
        // 원거리 공격 상태에 진입하면 즉시 멈춤
        _fsm.Owner.Rb.velocity = Vector2.zero;
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);
        float sqrDistance = _fsm.GetSqrDistanceToPlayer();

        // rangedAttackRange를 벗어나면 다시 Chase 상태로 전환
        if (sqrDistance > _sheetFSM.SO.rangedAttackRange * _sheetFSM.SO.rangedAttackRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.ChaseState);
            return;
        }

        // 쿨타임이 되면 원거리 공격 실행
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
