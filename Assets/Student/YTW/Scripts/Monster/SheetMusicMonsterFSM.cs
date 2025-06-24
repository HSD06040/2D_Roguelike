using UnityEngine;

public class SheetMusicMonsterFSM : MonsterFSM
{
    [field: SerializeField] public SheetMusicMonsterSO SO { get; private set; }

    public float ChaseRangeSqr { get; private set; }
    public float AttackRangeSqr { get; private set; }
    public SheetMusic_IdleState IdleState { get; private set; }
    public SheetMusic_ChaseState ChaseState { get; private set; }
    public SheetMusic_MeleeAttackState MeleeAttackState { get; private set; }
    public SheetMusic_DieState DieState { get; private set; }

    public float lastAttackTime;

    protected override void Awake()
    {
        base.Awake();

        Agent.speed = SO.moveSpeed;
        Agent.stoppingDistance = SO.meleeAttackRange;
        Owner.SetStats(SO.health, SO.attackPower);

        ChaseRangeSqr = SO.chaseRange * SO.chaseRange;
        AttackRangeSqr = SO.meleeAttackRange * SO.meleeAttackRange;

        IdleState = new SheetMusic_IdleState(this);
        ChaseState = new SheetMusic_ChaseState(this);
        MeleeAttackState = new SheetMusic_MeleeAttackState(this);
        DieState = new SheetMusic_DieState(this);
    }

    private void Start()
    {
        lastAttackTime = -SO.meleeAttackCooldown;
        StateMachine.Initialize(IdleState);
    }
    protected override void Update()
    {
        base.Update();

        if (Owner.CurrentHealth <= 0 && !(StateMachine.CurrentState is SheetMusic_DieState))
        {
            StateMachine.ChangeState(DieState);
        }
    }

    // 이 함수를 애니메이션 클립 이벤트에서 호출
    public override void AnimationAttackTrigger()
    {
        (StateMachine.CurrentState as SheetMusic_MeleeAttackState)?.TriggerAttack();
    }

    public void OnAttackAnimationFinished()
    {
        StateMachine.ChangeState(ChaseState);
    }

    // ---0--------------------


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
    public override void Enter()
    {
        _sheetFSM.Agent.isStopped = true;
        _sheetFSM.Owner.Animator.SetBool("IsChasing", false);
    }

    public override void Update()
    {
        if (_fsm.Player == null) return;
        _fsm.Owner.Flip(_fsm.Player);

        float sqrDistance = _fsm.GetSqrDistanceToPlayer();
        if (sqrDistance <= _sheetFSM.ChaseRangeSqr)
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

    public override void Update()
    {
        if (_fsm.Player == null)
        {
            _fsm.StateMachine.ChangeState(_sheetFSM.IdleState);
            return;
        }

        _fsm.Owner.Flip(_fsm.Player);

        float sqrDistance = _fsm.GetSqrDistanceToPlayer();

        // 플레이어가 공격 범위 안에 있을 때
        if (sqrDistance <= _sheetFSM.AttackRangeSqr)
        {
            // 추격을 멈춤
            _sheetFSM.Agent.isStopped = true;
            _sheetFSM.Owner.Animator.SetBool("IsChasing", false);

            // 공격 쿨타임이 되었는지 확인
            if (Time.time >= _sheetFSM.lastAttackTime + _sheetFSM.SO.meleeAttackCooldown)
            {
                // 쿨타임이 끝났다면 공격 상태로 전환
                _fsm.StateMachine.ChangeState(_sheetFSM.MeleeAttackState);
            }
            // 쿨타임 중이라면, 이 상태에 머무르며 제자리에서 대기합니다 (Idle 상태).
        }
        // 플레이어가 공격 범위 밖에 있을 때
        else
        {
            // 추격을 시작
            _sheetFSM.Agent.isStopped = false;
            _sheetFSM.Owner.Animator.SetBool("IsChasing", true);
            _sheetFSM.Agent.SetDestination(_fsm.Player.position);

            // 만약 플레이어가 추격 범위마저 벗어났다면, 비전투 Idle 상태로
            if (sqrDistance > _sheetFSM.ChaseRangeSqr)
            {
                _fsm.StateMachine.ChangeState(_sheetFSM.IdleState);
            }
        }
    }
    public override void Exit()
    {
        _sheetFSM.Agent.isStopped = true;
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
        _sheetFSM.Agent.isStopped = true;
        _sheetFSM.lastAttackTime = Time.time;
        _fsm.Owner.Animator.SetTrigger("Attack");
    }

    public override void Update()
    {
        if (_fsm.Player != null)
        {
            _fsm.Owner.Flip(_fsm.Player);
        }
    }

    public void TriggerAttack()
    {
        float sqrDistance = _fsm.GetSqrDistanceToPlayer();

        if (sqrDistance <= _sheetFSM.AttackRangeSqr && _fsm.Player != null)
        {
            Debug.Log($"플레이어에게 {_sheetFSM.SO.attackPower} 데미지를 입혔습니다");
            // TODO : 실제 플레이어에게 데미지를 주는 코드를 여기에 작성 예정
        }
    }

    public override void Exit()
    {
        if (_sheetFSM.Agent.isActiveAndEnabled)
        {
            _sheetFSM.Agent.isStopped = false;
        }
    }
}
public class SheetMusic_DieState : BaseState
{
    private SheetMusicMonsterFSM _sheetFSM;
    public SheetMusic_DieState(SheetMusicMonsterFSM fsm) : base(fsm)
    {
        _sheetFSM = fsm;
    }

    public override void Enter()
    {
        // 죽는 순간 모든 상호작용 비활성화
        _sheetFSM.Agent.isStopped = true;
        if (_fsm.Owner.TryGetComponent<Collider2D>(out var collider))
        {
            collider.enabled = false;
        }

        _fsm.Owner.Animator.SetTrigger("Die");

        //  아이템 드랍 로직
        DropItems();

        // 파괴 할건지, 비활성화 할건지, 오브젝트풀로 관리하는지??
    }

    private void DropItems()
    {
        if (_sheetFSM.SO.dropItemPrefab != null)
        {
            // 코인 프리팹 생성후 작업 예정\
        }
    }
}
