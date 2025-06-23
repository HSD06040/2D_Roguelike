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

    protected override void Update()
    {
        base.Update(); 
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        if (Player == null) return;

        // 플레이어 방향 벡터 계산
        Vector2 direction = Player.position - transform.position;

        // Atan2를 사용하여 각도를 계산하고, Radian을 Degree로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Z축을 기준으로 회전하는 Quaternion 생성 후 적용
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }


    private void OnDrawGizmosSelected()
    {
        if (SO == null) return;

        // 추격 범위 (노란색 원)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SO.chaseRange);

        // 몬스터의 현재 정면 방향 (이제 transform.right가 항상 정면임)
        Vector3 forwardDir = transform.right;

        // 부채꼴의 시작점 계산
        Quaternion rotation = Quaternion.AngleAxis(-SO.meleeAttackAngle / 2, Vector3.forward);
        Vector3 arcStart = rotation * forwardDir;

#if UNITY_EDITOR
        // 공격 사거리와 시야각을 함께 표시 (파란색 부채꼴)
        Handles.color = new Color(0, 0, 1, 0.2f);
        Handles.DrawSolidArc(transform.position, Vector3.forward, arcStart, SO.meleeAttackAngle, SO.meleeAttackRange);

        // 시야각 외곽선 (진한 파란색)
        Handles.color = Color.blue;
        Handles.DrawWireArc(transform.position, Vector3.forward, arcStart, SO.meleeAttackAngle, SO.meleeAttackRange);
#endif
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
        // _fsm.Owner.Flip(_fsm.Player); // Flip 호출 제거

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
        // _fsm.Owner.Flip(_fsm.Player); // Flip 호출 제거

        if (_fsm.Player != null)
        {
            _sheetFSM.Agent.SetDestination(_fsm.Player.position);
        }

        if (!_sheetFSM.Agent.pathPending && _sheetFSM.Agent.remainingDistance <= _sheetFSM.Agent.stoppingDistance)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.MeleeAttackState);
            return;
        }

        float sqrDistance = _fsm.GetSqrDistanceToPlayer();
        if (sqrDistance > _sheetFSM.SO.chaseRange * _sheetFSM.SO.chaseRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.IdleState);
            return;
        }
    }

    public override void Exit()
    {
        if (_sheetFSM.Agent.isActiveAndEnabled)
        {
            _sheetFSM.Agent.isStopped = true;
            _sheetFSM.Agent.ResetPath();
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
        if (_sheetFSM.Agent.isActiveAndEnabled)
        {
            _sheetFSM.Agent.isStopped = true;
            _sheetFSM.Agent.ResetPath();
        }
    }

    public override void Update()
    {
        // _fsm.Owner.Flip(_fsm.Player); // Flip 호출 제거

        if (!IsPlayerInAttackZone())
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.ChaseState);
            return;
        }

        if (Time.time >= _sheetFSM.lastAttackTime + _sheetFSM.SO.meleeAttackCooldown)
        {
            PerformMeleeAttack();
        }
    }

    private bool IsPlayerInAttackZone()
    {
        if (_fsm.Player == null) return false;

        float sqrDistance = _fsm.GetSqrDistanceToPlayer();
        if (sqrDistance > _sheetFSM.SO.meleeAttackRange * _sheetFSM.SO.meleeAttackRange)
        {
            return false;
        }

        Vector2 directionToPlayer = _fsm.Player.position - _fsm.transform.position;

        Vector2 monsterForward = _fsm.transform.right;

        float angle = Vector2.Angle(monsterForward, directionToPlayer);
        if (angle > _sheetFSM.SO.meleeAttackAngle / 2)
        {
            return false;
        }

        return true;
    }

    private void PerformMeleeAttack()
    {
        _sheetFSM.lastAttackTime = Time.time;
        Debug.Log("근접 공격");
    }
}
