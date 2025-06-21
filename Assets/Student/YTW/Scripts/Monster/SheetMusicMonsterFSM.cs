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

    // 공격 쿨타임을 관리할 타이머
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

        // 원거리 공격 범위 (빨간색)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SO.meleeAttackRange);

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

        // 플레이어의 위치를 목적지로 계속 설정하여 추적
        if (_fsm.Player != null)
        {
            _sheetFSM.Agent.SetDestination(_fsm.Player.position);
        }

        if (!_sheetFSM.Agent.pathPending && _sheetFSM.Agent.remainingDistance <= _sheetFSM.Agent.stoppingDistance)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.MeleeAttackState);
            return;
        }

        // 추격 범위를 벗어나면 대기 상태로 전환 (이 부분은 그대로 둡니다)
        float sqrDistance = _fsm.GetSqrDistanceToPlayer(); // 위의 조건문과 별개로 거리는 다시 계산해야 합니다.
        if (sqrDistance > _sheetFSM.SO.chaseRange * _sheetFSM.SO.chaseRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.IdleState);
            return;
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

public class SheetMusic_MeleeAttackState : BaseState
{
    private SheetMusicMonsterFSM _sheetFSM;

    public SheetMusic_MeleeAttackState(SheetMusicMonsterFSM fsm) : base(fsm)
    {
        _sheetFSM = fsm;
    }
    public override void Enter()
    {
        // 공격 상태 진입 시 NavMeshAgent를 정지시켜 제자리에 멈추게 함
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

        // 공격 범위를 벗어나면 다시 추격 상태로 전환
        if (sqrDistance > _sheetFSM.SO.meleeAttackRange * _sheetFSM.SO.meleeAttackRange) // meleeAttackRange로 변경
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.ChaseState);
            return;
        }

        // 쿨타임이 되면 근접 공격 실행
        if (Time.time >= _sheetFSM.lastAttackTime + _sheetFSM.SO.meleeAttackCooldown) // meleeAttackCooldown으로 변경
        {
            PerformMeleeAttack();
        }
    }

    private void PerformMeleeAttack()
    {
        _sheetFSM.lastAttackTime = Time.time;

        // TODO: 여기에 실제 근접 공격 로직을 구현해야 합니다.
        // 공격 애니메이션 재생, 플레이어에게 데미지 전달 등
        // PlayerController player = _fsm.Player.GetComponent<PlayerController>();
        // player.TakeDamage(_sheetFSM.SO.attackPower);

        Debug.Log("근접 공격");
    }
}
