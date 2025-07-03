using System.Collections;
using UnityEngine;

public class ExplosionMonsterFSM : MonsterFSM
{
    [field: SerializeField] public ExplosionMonsterSO SO { get; private set; }
    [SerializeField] public Material whiteFlashMaterial;
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Material OriginalMaterial { get; private set; }

    public Explosion_IdleState IdleState { get; private set; }
    public Explosion_ChaseState ChaseState { get; private set; }
    public Explosion_ChargeState ChargeState { get; private set; }
    public Explosion_ExplodeState ExplodeState { get; private set; }

    public float ChaseRangeSqr { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Agent.stoppingDistance = 0.1f;
        Owner.SetStats(SO.health, SO.attackPower);
        Agent.speed = SO.moveSpeed;

        SpriteRenderer = Owner.GetComponentInChildren<SpriteRenderer>();
        OriginalMaterial = SpriteRenderer.material;
        ChaseRangeSqr = SO.chaseRange * SO.chaseRange;

        IdleState = new Explosion_IdleState(this);
        ChaseState = new Explosion_ChaseState(this);
        ChargeState = new Explosion_ChargeState(this);
        ExplodeState = new Explosion_ExplodeState(this);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        if (Owner.CurrentHealth <= 0 && !(StateMachine.CurrentState is Explosion_ExplodeState))
        {
            StateMachine.ChangeState(ExplodeState);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // SO�� �Ҵ���� �ʾ����� ���� ������ ���� �������� ����
        if (SO == null) return;

        // �߰� ���� (����� ��)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, SO.chaseRange);

        // ����(���� ����) ���� (������ ��)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SO.attackRange);

        // Ÿ���� ���� ���� (��Ȳ��)
        Gizmos.color = new Color(1.0f, 0.5f, 0.0f); 

        float baseScaleX = SO.explosionScaleX;
        float baseScaleY = SO.explosionScaleY;
        float masterRadius = SO.explosionRadius;

        float longerSide = Mathf.Max(baseScaleX, baseScaleY);
        if (longerSide == 0) return;
        float scaleMultiplier = (masterRadius * 2) / longerSide;

        float finalRadiusX = (baseScaleX * scaleMultiplier) / 2;
        float finalRadiusY = (baseScaleY * scaleMultiplier) / 2;

        int segments = 32;
        Vector3 previousPoint = Vector3.zero;
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * (360f / segments) * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * finalRadiusX;
            float y = Mathf.Sin(angle) * finalRadiusY;
            Vector3 currentPoint = transform.position + new Vector3(x, y, 0);

            if (i > 0)
            {
                Gizmos.DrawLine(previousPoint, currentPoint);
            }
            previousPoint = currentPoint;
        }
    }

}

public class Explosion_IdleState : BaseState
{
    private ExplosionMonsterFSM _explosionMonsterFSM;
    public Explosion_IdleState(ExplosionMonsterFSM fsm) : base(fsm) => _explosionMonsterFSM = fsm;
    public override void Enter()
    {
        _explosionMonsterFSM.Owner.Animator.SetBool("IsChasing", false);
    }
    public override void Update()
    {
        _explosionMonsterFSM.Owner.Flip(_explosionMonsterFSM.Player);

        if (_explosionMonsterFSM.GetSqrDistanceToPlayer() <= _explosionMonsterFSM.ChaseRangeSqr)
        {
            _explosionMonsterFSM.StateMachine.ChangeState(_explosionMonsterFSM.ChaseState);
        }
    }

}
public class Explosion_ChaseState : BaseState
{
    private ExplosionMonsterFSM _explosionMonsterFSM;
    public Explosion_ChaseState(ExplosionMonsterFSM fsm) : base(fsm) => _explosionMonsterFSM = fsm;
    public override void Enter()
    {
        _explosionMonsterFSM.Agent.speed = _explosionMonsterFSM.SO.moveSpeed;
        _explosionMonsterFSM.Owner.Animator.SetBool("IsChasing", true);
    }

    public override void Update()
    {
        _explosionMonsterFSM.Owner.Flip(_explosionMonsterFSM.Player);

        if (_explosionMonsterFSM.Player == null) return;
        _explosionMonsterFSM.Agent.SetDestination(_explosionMonsterFSM.Player.position);

        if (_explosionMonsterFSM.GetSqrDistanceToPlayer() <= _explosionMonsterFSM.SO.attackRange * _explosionMonsterFSM.SO.attackRange)
        {
            _explosionMonsterFSM.StateMachine.ChangeState(_explosionMonsterFSM.ChargeState);
        }
    }
}

public class Explosion_ChargeState : BaseState
{
    private ExplosionMonsterFSM _explosionMonsterFSM;
    private float _chargeTimer;
    private Coroutine _blinkCoroutine;
    public Explosion_ChargeState(ExplosionMonsterFSM fsm) : base(fsm) => _explosionMonsterFSM = fsm;
    public override void Enter()
    {
        _chargeTimer = _explosionMonsterFSM.SO.chargeTime;
        _explosionMonsterFSM.Agent.speed = _explosionMonsterFSM.SO.moveSpeed * _explosionMonsterFSM.SO.chargeSpeedMultiplier;
        _explosionMonsterFSM.Owner.Animator.SetBool("IsChasing", true);

        _blinkCoroutine = _explosionMonsterFSM.StartCoroutine(BlinkEffect());
    }
    private IEnumerator BlinkEffect()
    {
        while (true)
        {
            if (_explosionMonsterFSM.whiteFlashMaterial != null && _explosionMonsterFSM.SpriteRenderer != null)
            {
                _explosionMonsterFSM.SpriteRenderer.material = _explosionMonsterFSM.whiteFlashMaterial;
            }
            yield return new WaitForSeconds(0.1f);

            if (_explosionMonsterFSM.OriginalMaterial != null && _explosionMonsterFSM.SpriteRenderer != null)
            {
                _explosionMonsterFSM.SpriteRenderer.material = _explosionMonsterFSM.OriginalMaterial;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void Update()
    {
        _explosionMonsterFSM.Owner.Flip(_explosionMonsterFSM.Player);
        _chargeTimer -= Time.deltaTime;
        if (_explosionMonsterFSM.Player != null)
        {
            _explosionMonsterFSM.Agent.SetDestination(_explosionMonsterFSM.Player.position);
        }

        if (_chargeTimer <= 0f)
        {
            _explosionMonsterFSM.StateMachine.ChangeState(_explosionMonsterFSM.ExplodeState);
        }
    }
    public override void Exit()
    {

        if (_blinkCoroutine != null)
        {
            _explosionMonsterFSM.StopCoroutine(_blinkCoroutine);
        }
        _explosionMonsterFSM.SpriteRenderer.material = _explosionMonsterFSM.OriginalMaterial;
    }

}

public class Explosion_ExplodeState : BaseState
{
    private ExplosionMonsterFSM _explosionMonsterFSM;
    public Explosion_ExplodeState(ExplosionMonsterFSM fsm) : base(fsm) => _explosionMonsterFSM = fsm;

    public override void Enter()
    {
        _explosionMonsterFSM.Agent.isStopped = true;
        GameObject indicator = Object.Instantiate(_explosionMonsterFSM.SO.explosionIndicatorPrefab, _explosionMonsterFSM.transform.position, Quaternion.identity, _explosionMonsterFSM.transform);
        indicator.GetComponent<ExplosionIndicatorController>()?.SetSize(_explosionMonsterFSM.SO.explosionRadius, _explosionMonsterFSM.SO.explosionScaleX, _explosionMonsterFSM.SO.explosionScaleY);

        _explosionMonsterFSM.Owner.Animator.SetTrigger("Explode");
        _explosionMonsterFSM.StartCoroutine(ExplodeAfterDelay(_explosionMonsterFSM.SO.explosionDelay));
    }
    private IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_explosionMonsterFSM == null || _explosionMonsterFSM.Player == null)
        {
            Object.Destroy(_explosionMonsterFSM.gameObject);
            yield break;
        }

        float baseScaleX = _explosionMonsterFSM.SO.explosionScaleX;
        float baseScaleY = _explosionMonsterFSM.SO.explosionScaleY;
        float masterRadius = _explosionMonsterFSM.SO.explosionRadius;

        float longerSide = Mathf.Max(baseScaleX, baseScaleY);
        if (longerSide == 0)
        {
            Object.Destroy(_explosionMonsterFSM.gameObject);
            yield break;
        }

        float scaleMultiplier = (masterRadius * 2) / longerSide;
        float finalRadiusX = (baseScaleX * scaleMultiplier) / 2;
        float finalRadiusY = (baseScaleY * scaleMultiplier) / 2;

        // �÷��̾��� ��ġ ���
        Vector2 relativePos = _explosionMonsterFSM.Player.position - _explosionMonsterFSM.transform.position;

        // Ÿ�� ���������� �÷��̾ ���� �ȿ� �ִ��� ����
        // (0���� ������ ����). �׳� ����Ʈ�� �ݶ��̴��ؼ� �ϴ°� �� �������Ű��⵵ �մϴ�..
        if (finalRadiusX > 0 && finalRadiusY > 0)
        {
            float check = (relativePos.x * relativePos.x) / (finalRadiusX * finalRadiusX) + (relativePos.y * relativePos.y) / (finalRadiusY * finalRadiusY);

            if (check <= 1)
            {
                // ������ ����
                if (_explosionMonsterFSM.Player.TryGetComponent<IDamagable>(out var damageable))
                {
                    damageable.TakeDamage(_explosionMonsterFSM.Owner.AttackPower);
                    Debug.Log($"{_explosionMonsterFSM.Player.name}���� Ÿ���� ���߷� ������ {_explosionMonsterFSM.Owner.AttackPower}");
                }
            }
        }


        Object.Instantiate(_explosionMonsterFSM.SO.explosionEffectPrefab, _explosionMonsterFSM.transform.position, Quaternion.identity);
        _explosionMonsterFSM.Owner.DropCoin(_explosionMonsterFSM.SO);
    }
}
