using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RangedShotMonsterFSM : MonsterFSM
{
    [field: SerializeField] public RangedShotMonsterSO SO { get; private set; }
    [SerializeField] public Transform firePoint;
    public RangedShotMonster_IdleState IdleState { get; private set; }
    public RangedShot_RepositionState RepositionState { get; private set; }
    public RangedShotMonster_AttackState AttackState { get; private set; }
    public RangedShotMonster_DieState DieState { get; private set; }

    public float lastAttackTime;

    protected override void Awake()
    {
        base.Awake();

        Agent.stoppingDistance = 0.1f;
        Agent.speed = SO.moveSpeed;
        Owner.SetStats(SO.health, SO.attackPower);

        IdleState = new RangedShotMonster_IdleState(this);
        RepositionState = new RangedShot_RepositionState(this);
        AttackState = new RangedShotMonster_AttackState(this);
        DieState = new RangedShotMonster_DieState(this);
    }

    private void Start()
    {
        lastAttackTime = -SO.attackCooldown;
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        if (Owner.CurrentHealth <= 0 && !(StateMachine.CurrentState is RangedShotMonster_DieState))
        {
            StateMachine.ChangeState(DieState);
        }
    }

    public override void AnimationAttackTrigger()
    {
        (StateMachine.CurrentState as RangedShotMonster_AttackState)?.TriggerAttack();
    }

    public override void OnAttackAnimationFinished()
    {
        StateMachine.ChangeState(RepositionState);
    }

    public void FindAndSetNewWanderPosition()
    {
        if (Player == null) return;
        for (int i = 0; i < 10; i++)
        {
            float randomAngle = Random.Range(0, 2 * Mathf.PI);
            float randomDistance = Random.Range(SO.minRepositionDistance, SO.maxRepositionDistance);

            Vector2 offset = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * randomDistance;
            Vector3 randomPoint = Player.position + (Vector3)offset;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas))
            {
                Agent.SetDestination(hit.position);
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (SO == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SO.detectionRange);
        if (Application.isPlaying && Player != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(Player.position, SO.minRepositionDistance);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(Player.position, SO.maxRepositionDistance);
        }
    }
}

public class RangedShotMonster_IdleState : BaseState
{
    private RangedShotMonsterFSM _rangedShotMonsterFSM;
    public RangedShotMonster_IdleState(RangedShotMonsterFSM fsm) : base(fsm) => _rangedShotMonsterFSM = fsm;

    public override void Enter()
    {
        _rangedShotMonsterFSM.Owner.Animator.SetBool("IsChasing", false);
    }

    public override void Update()
    {
        if (_rangedShotMonsterFSM.GetSqrDistanceToPlayer() <= _rangedShotMonsterFSM.SO.detectionRange * _rangedShotMonsterFSM.SO.detectionRange)
        {
            _rangedShotMonsterFSM.StateMachine.ChangeState(_rangedShotMonsterFSM.RepositionState);
        }
    }
}

public class RangedShot_RepositionState : BaseState
{
    private RangedShotMonsterFSM _rangedShotMonsterFSM;
    public RangedShot_RepositionState(RangedShotMonsterFSM fsm) : base(fsm) => _rangedShotMonsterFSM = fsm;

    public override void Enter()
    {
        _rangedShotMonsterFSM.Agent.isStopped = false;
        _rangedShotMonsterFSM.Owner.Animator.SetBool("IsChasing", true);
        _rangedShotMonsterFSM.FindAndSetNewWanderPosition();
    }

    public override void Update()
    {
        if (!_rangedShotMonsterFSM.Agent.pathPending && _rangedShotMonsterFSM.Agent.remainingDistance > _rangedShotMonsterFSM.Agent.stoppingDistance)
        {
            _rangedShotMonsterFSM.Owner.Flip(_rangedShotMonsterFSM.Player);
        }

        else if (!_rangedShotMonsterFSM.Agent.pathPending && _rangedShotMonsterFSM.Agent.remainingDistance <= _rangedShotMonsterFSM.Agent.stoppingDistance)
        {
            _rangedShotMonsterFSM.Agent.isStopped = true;
            _rangedShotMonsterFSM.Owner.Animator.SetBool("IsChasing", false);


            if (Time.time >= _rangedShotMonsterFSM.lastAttackTime + _rangedShotMonsterFSM.SO.attackCooldown)
            {
                _rangedShotMonsterFSM.StateMachine.ChangeState(_rangedShotMonsterFSM.AttackState);
            }
        }
    }
}

public class RangedShotMonster_AttackState : BaseState
{
    private RangedShotMonsterFSM _rangedShotMonsterFSM;
    private Coroutine _fireCoroutine;
    private Vector2 _lockedOnDirection;
    public RangedShotMonster_AttackState(RangedShotMonsterFSM fsm) : base(fsm) => _rangedShotMonsterFSM = fsm;

    public override void Enter()
    {
        _rangedShotMonsterFSM.Agent.isStopped = true;
        _rangedShotMonsterFSM.Agent.ResetPath();
        
        _rangedShotMonsterFSM.Owner.Animator.SetTrigger("Attack");
    }

    public override void Update()
    { 
        _rangedShotMonsterFSM.Owner.Flip(_rangedShotMonsterFSM.Player);
    }

    public void TriggerAttack()
    {
        if (_fireCoroutine != null) return;

        if (_rangedShotMonsterFSM.SO.aimType == RangedShotMonsterSO.AimType.LockOnPlayer && _rangedShotMonsterFSM.Player != null)
        {
            _lockedOnDirection = (_rangedShotMonsterFSM.Player.position - _rangedShotMonsterFSM.transform.position).normalized;
        }

        _fireCoroutine = _rangedShotMonsterFSM.StartCoroutine(FireSequence());
    }

    private IEnumerator FireSequence()
    {
        _rangedShotMonsterFSM.lastAttackTime = Time.time;
        // SO�� ������ shotCount ��ŭ ������ �ݺ�

        float currentSpiralAngle = 0f;

        for (int i = 0; i < _rangedShotMonsterFSM.SO.shotCount; i++)
        {
            FireOneSpread(currentSpiralAngle);

            currentSpiralAngle += _rangedShotMonsterFSM.SO.spiralRotationSpeed;

            //  SO�� ������ ���� �߻� ����
            yield return new WaitForSeconds(_rangedShotMonsterFSM.SO.timeBetweenShots);

        }
        _rangedShotMonsterFSM.OnAttackAnimationFinished();
    }

    private void FireOneSpread(float currentSpiralAngle)
    {
        if (_rangedShotMonsterFSM.SO.projectilePrefab == null || _rangedShotMonsterFSM.Player == null) return;
        // ���Ϳ��� �÷��̾ ���ϰų� �ڽ��� �߽��� ������ ���. �� ������ ��ä���� �߽�
        Vector2 centerDirection;

        switch (_rangedShotMonsterFSM.SO.aimType)
        {
            case RangedShotMonsterSO.AimType.Player:
                // �÷��̾ ���ϴ� ������ �߽����� ����
                centerDirection = (_rangedShotMonsterFSM.Player.position - _rangedShotMonsterFSM.transform.position).normalized;
                break;

            // ���� ���� 
            case RangedShotMonsterSO.AimType.LockOnPlayer:
                // �̸� ����ص� ���� ���
                centerDirection = _lockedOnDirection;
                break;

            case RangedShotMonsterSO.AimType.Self:
                // ���Ͱ� ���� �ٶ󺸴� ���� ������ �߽����� ����
                centerDirection = _rangedShotMonsterFSM.transform.right * _rangedShotMonsterFSM.Owner.FacingDirection;
                break;

            default:
                centerDirection = Vector2.right; // �⺻��
                break;
        }
        // �⺻ ������ ������ ���� ���� ������ ���ؼ� ���� ȸ������ ����
        float totalRotation = _rangedShotMonsterFSM.SO.fireAngleOffset + currentSpiralAngle;
        // ���� ���͸� fireAngleOffset ������ ����
        centerDirection = Quaternion.Euler(0, 0, totalRotation) * centerDirection;
        float angleStep;
        float startAngle;

        if (_rangedShotMonsterFSM.SO.spreadAngle >= 360)
        {
            // ���� ������ ���: ��ü ������ �Ѿ� ������ ���� (360 / 4 = 90�� ����)
            angleStep = _rangedShotMonsterFSM.SO.spreadAngle / _rangedShotMonsterFSM.SO.projectileCount;
            // ���� ������ 0���� �����Ͽ� ������� �߻縦 ����
            startAngle = 0f;
        }
        else
        {
            // ��ä�� ������ ���:  �� ��ź ������ ���� ������ ���
            angleStep = _rangedShotMonsterFSM.SO.projectileCount > 1 ? _rangedShotMonsterFSM.SO.spreadAngle / (_rangedShotMonsterFSM.SO.projectileCount - 1) : 0;
            // ��ü ������ ���ݸ�ŭ ���̳ʽ� ȸ���� ��ġ�� ��ä���� ������
            startAngle = -_rangedShotMonsterFSM.SO.spreadAngle / 2;
        }

        // SO�� ������ projectileCount ��ŭ �ݺ��ϸ鼭 �Ѿ��� �ϳ��� �߻�
        for (int i = 0; i < _rangedShotMonsterFSM.SO.projectileCount; i++)
        {
            // ���� �Ѿ��� �߻�� ������ ��� (���� ���� + (���� ���� * ����))
            float currentAngle = startAngle + (angleStep * i);
            // �߽� ���� ����(centerDirection)�� ��� ����� currentAngle��ŭ ȸ������, ���� �߻� ���� ����
            Vector2 fireDirection = Quaternion.Euler(0, 0, currentAngle) * centerDirection;

            // ������ �̸��� ������Ʈ Ǯ �� �߻�ü tag�̸��� ���ƾ���
            string projectileTag = _rangedShotMonsterFSM.SO.projectilePrefab.name;
            GameObject projectile = Manager.Resources.Instantiate(_rangedShotMonsterFSM.SO.projectilePrefab, _rangedShotMonsterFSM.firePoint.position, true);

            if (projectile != null)
            {
                projectile.transform.position = _rangedShotMonsterFSM.firePoint.position;
                projectile.GetComponent<Projectile_Controller>().Initialize(
                    fireDirection,                    
                    _rangedShotMonsterFSM.SO.projectileSpeed,               
                    _rangedShotMonsterFSM.Owner.AttackPower,
                    projectileTag
                    );
            }
        }
    }

    public override void Exit()
    {
        if (_fireCoroutine != null)
        {
            _rangedShotMonsterFSM.StopCoroutine(_fireCoroutine);
            _fireCoroutine = null;
        }
    }
}

public class RangedShotMonster_DieState : BaseState
{
    private RangedShotMonsterFSM _rangedShotMonsterFSM;
    public RangedShotMonster_DieState(RangedShotMonsterFSM fsm) : base(fsm) => _rangedShotMonsterFSM = fsm;

    public override void Enter()
    {
        _rangedShotMonsterFSM.Agent.isStopped = true;
        if (_rangedShotMonsterFSM.Owner.TryGetComponent<Collider2D>(out var collider))
        {
            collider.enabled = false;
        }

        _rangedShotMonsterFSM.Owner.Animator.SetTrigger("Die");
        _rangedShotMonsterFSM.Owner.DropCoin(_rangedShotMonsterFSM.SO);
    }

}