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

    // �� �Լ��� �ִϸ��̼� Ŭ�� �̺�Ʈ���� ȣ��
    public override void AnimationAttackTrigger()
    {
        (StateMachine.CurrentState as SheetMusic_MeleeAttackState)?.TriggerAttack();
    }

    public override void OnAttackAnimationFinished()
    {
        StateMachine.ChangeState(ChaseState);
    }

    // ---0--------------------


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
    public override void Enter()
    {
        _sheetFSM.Agent.isStopped = true;
        _sheetFSM.Owner.Animator.SetBool("IsChasing", false);
    }

    public override void Update()
    {
        if (_fsm.Player == null) return;
        _sheetFSM.Owner.Flip(_sheetFSM.Player);

        float sqrDistance = _sheetFSM.GetSqrDistanceToPlayer();
        if (sqrDistance <= _sheetFSM.ChaseRangeSqr)
        {
            _sheetFSM.StateMachine.ChangeState(_sheetFSM.ChaseState);
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
        if (_sheetFSM.Player == null)
        {
            _sheetFSM.StateMachine.ChangeState(_sheetFSM.IdleState);
            return;
        }

        _sheetFSM.Owner.Flip(_sheetFSM.Player);

        float sqrDistance = _sheetFSM.GetSqrDistanceToPlayer();

        // �÷��̾ ���� ���� �ȿ� ���� ��
        if (sqrDistance <= _sheetFSM.AttackRangeSqr)
        {
            // �߰��� ����
            _sheetFSM.Agent.isStopped = true;
            _sheetFSM.Owner.Animator.SetBool("IsChasing", false);

            // ���� ��Ÿ���� �Ǿ����� Ȯ��
            if (Time.time >= _sheetFSM.lastAttackTime + _sheetFSM.SO.meleeAttackCooldown)
            {
                // ��Ÿ���� �����ٸ� ���� ���·� ��ȯ
                _sheetFSM.StateMachine.ChangeState(_sheetFSM.MeleeAttackState);
            }
            // ��Ÿ�� ���̶��, �� ���¿� �ӹ����� ���ڸ����� ����մϴ� (Idle ����).
        }
        // �÷��̾ ���� ���� �ۿ� ���� ��
        else
        {
            // �߰��� ����
            _sheetFSM.Agent.isStopped = false;
            _sheetFSM.Owner.Animator.SetBool("IsChasing", true);
            _sheetFSM.Agent.SetDestination(_sheetFSM.Player.position);

            // ���� �÷��̾ �߰� �������� ����ٸ�, ������ Idle ���·�
            if (sqrDistance > _sheetFSM.ChaseRangeSqr)
            {
                _sheetFSM.StateMachine.ChangeState(_sheetFSM.IdleState);
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
        _sheetFSM.Owner.Animator.SetTrigger("Attack");
        if (_sheetFSM.Player != null)
        {
            _sheetFSM.Owner.Flip(_sheetFSM.Player);
        }
    }

    public void TriggerAttack()
    {
        LayerMask playerLayer = LayerMask.GetMask("Player");

        Collider2D hit = Physics2D.OverlapCircle(
            _sheetFSM.transform.position,
            _sheetFSM.SO.meleeAttackRange,
            playerLayer
        );

        if (hit != null)
        {
            // TryGetComponent는 Player가 IDamagable을 가지고 있다면 true를 반환하고 damageable 변수에 값을 할
            if (hit.TryGetComponent<IDamagable>(out IDamagable damageable))
            {
                damageable.TakeDamage(_sheetFSM.SO.attackPower);
                Debug.Log($"{hit.name}에게 근접 공격으로 {_sheetFSM.SO.attackPower} 데미지!");
            }
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
        // �״� ���� ��� ��ȣ�ۿ� ��Ȱ��ȭ
        _sheetFSM.Agent.isStopped = true;
        if (_sheetFSM.Owner.TryGetComponent<Collider2D>(out var collider))
        {
            collider.enabled = false;
        }

        _sheetFSM.Owner.Animator.SetTrigger("Die");

        //  ������ ��� ����
        DropItems();

        // �ı� �Ұ���, ��Ȱ��ȭ �Ұ���, ������ƮǮ�� �����ϴ���??
    }

    private void DropItems()
    {
        if (_sheetFSM.SO.dropItemPrefab != null)
        {
            // ���� ������ ������ �۾� ����\
        }
    }
}
