#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class SheetMusicMonsterFSM : MonsterFSM
{
    [field: SerializeField] public SheetMusicMonsterSO SO { get; private set; }
    public float ChaseRangeSqr { get; private set; }
    public float AttackRangeSqr { get; private set; }
    public float AttackAngleCosine { get; private set; }
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
        AttackAngleCosine = Mathf.Cos(SO.meleeAttackAngle * 0.5f * Mathf.Deg2Rad);

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

        if (Owner == null) return;

        // �߰� ���� (����� ��)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SO.chaseRange);

#if UNITY_EDITOR // 이 코드는 유니티 에디터에서만 실행

        Handles.color = new Color(1, 0, 0, 0.2f);

        Vector3 forward = transform.right * Owner.FacingDirection;

        Vector3 startAngle = Quaternion.Euler(0, 0, -SO.meleeAttackAngle / 2) * forward;

        Handles.DrawSolidArc(
            transform.position, 
            Vector3.forward,     
            startAngle,          
            SO.meleeAttackAngle, 
            SO.meleeAttackRange  
        );

        Handles.color = new Color(1, 0.2f, 0.2f, 0.5f);
        Handles.DrawWireArc(transform.position, Vector3.forward, startAngle, SO.meleeAttackAngle, SO.meleeAttackRange, 2f);

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
    public override void Enter()
    {
        _sheetFSM.Agent.isStopped = true;
        _sheetFSM.Owner.Animator.SetBool("IsChasing", false);
    }

    public override void Update()
    {
        if (__spreadFSM.Player == null) return;
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
        // 플레이어 오브젝트가 존재하는지 먼저 확인
        if (_sheetFSM.Player == null) return;

        Transform playerTransform = _sheetFSM.Player;
        Transform monsterTransform = _sheetFSM.transform;

        // 플레이어가 공격 반경 안에 있는지 확인 (1차 필터링)
        float sqrDistance = (playerTransform.position - monsterTransform.position).sqrMagnitude;
        if (sqrDistance <= _sheetFSM.AttackRangeSqr)
        {
            // 플레이어가 공격 각도 안에 있는지 확인 (2차 필터링)
            Vector2 targetDir = (playerTransform.position - monsterTransform.position).normalized;
            Vector2 monsterForward = monsterTransform.right * _sheetFSM.Owner.FacingDirection;

            float dot = Vector2.Dot(monsterForward, targetDir);

            // 내적 값이 미리 계산해둔 코사인 값보다 크면 부채꼴 안에 있음
            if (dot >= _sheetFSM.AttackAngleCosine)
            {
                // 모든 조건을 통과하면 데미지 처리
                if (playerTransform.TryGetComponent<IDamagable>(out IDamagable damageable))
                {
                    damageable.TakeDamage(_sheetFSM.SO.attackPower);
                    Debug.Log($"{playerTransform.name}에게 부채꼴 공격 명중. 데미지 : {_sheetFSM.SO.attackPower}");
                }
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
