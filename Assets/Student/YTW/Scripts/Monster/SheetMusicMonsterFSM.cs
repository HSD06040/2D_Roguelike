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

        // 추격 범위 (노란색 원)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SO.chaseRange);

        // 공격 범위 (빨간색 원)
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

        //  플레이어가 공격 범위 트리거 안에 들어오면 공격 상태로 전환
        if (_sheetFSM.IsPlayerInAttackRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.MeleeAttackState);
            return;
        }

        // 추격 범위를 벗어나면 대기 상태로 전환
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
        _sheetFSM.Agent.isStopped = true; // 공격 중에는 이동 멈춤
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);

        // [수정] 플레이어가 공격 범위를 벗어나면 즉시 다시 추격 상태로 전환
        if (!_sheetFSM.IsPlayerInAttackRange)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.ChaseState);
            return;
        }

        // 공격 쿨타임이 다 되었다면 공격 실행
        if (Time.time >= _sheetFSM.lastAttackTime + _sheetFSM.SO.meleeAttackCooldown)
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        _sheetFSM.lastAttackTime = Time.time;

        // 공격 애니메이션, 사운드, 데미지 처리 로직
        // 공격 예시
        // 1. 공격 애니메이션 재생 (Animator.SetTrigger("Attack"))
        // 2. 공격 효과음 재생 (AudioSource.PlayOneShot(attackSound))
        // 3. 플레이어에게 데미지 전달
        //  PlayerHealth playerHealth = _fsm.Player.GetComponent<PlayerHealth>();
        //    
        //  player.TakeDamage(_sheetFSM.SO.attackPower);
        Debug.Log("공격 실행");
    }

    public override void Exit()
    {
        if (_sheetFSM.Agent.isActiveAndEnabled)
        {
            _sheetFSM.Agent.isStopped = false;
        }
    }
}