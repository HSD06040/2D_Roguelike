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

    public void OnAttackAnimationFinished()
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
                _fsm.StateMachine.ChangeState(_sheetFSM.MeleeAttackState);
            }
            // ��Ÿ�� ���̶��, �� ���¿� �ӹ����� ���ڸ����� ����մϴ� (Idle ����).
        }
        // �÷��̾ ���� ���� �ۿ� ���� ��
        else
        {
            // �߰��� ����
            _sheetFSM.Agent.isStopped = false;
            _sheetFSM.Owner.Animator.SetBool("IsChasing", true);
            _sheetFSM.Agent.SetDestination(_fsm.Player.position);

            // ���� �÷��̾ �߰� �������� ����ٸ�, ������ Idle ���·�
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
            Debug.Log($"�÷��̾�� {_sheetFSM.SO.attackPower} �������� �������ϴ�");
            // TODO : ���� �÷��̾�� �������� �ִ� �ڵ带 ���⿡ �ۼ� ����
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
        if (_fsm.Owner.TryGetComponent<Collider2D>(out var collider))
        {
            collider.enabled = false;
        }

        _fsm.Owner.Animator.SetTrigger("Die");

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
