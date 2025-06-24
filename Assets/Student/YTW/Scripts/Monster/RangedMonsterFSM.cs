using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedMonsterFSM : MonsterFSM
{
    [field: SerializeField] public RangedMonsterSO SO { get; private set; }

    public NavMeshAgent Agent { get; private set; }

    public Ranged_IdleState IdleState { get; private set; }
    public Ranged_RepositionState RepositionState { get; private set; }
    public Ranged_AttackState AttackState { get; private set; }

    public float lastAttackTime; 

    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();

        
        Agent.stoppingDistance = 0.1f;
        Agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        Agent.speed = SO.moveSpeed;
        Owner.SetStats(SO.health, SO.attackPower);

        IdleState = new Ranged_IdleState(this);
        RepositionState = new Ranged_RepositionState(this);
        AttackState = new Ranged_AttackState(this);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }
    public void FindAndSetNewWanderPosition()
    {
        if (Player == null) return;

        float halfWidth = SO.repositionBoxWidth / 2;
        float halfHeight = SO.repositionBoxHeight / 2;

        Vector3 randomPoint = Player.position + new Vector3(Random.Range(-halfWidth, halfWidth), Random.Range(-halfHeight, halfHeight), 0);

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
        {
            Agent.SetDestination(hit.position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (SO == null) return;

        // �÷��̾� Ž�� ���� (������ ��)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SO.detectionRange);

        // ���Ͱ� ������ ���� (����� �簢��)
        if (Application.isPlaying && Player != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 boxCenter = Player.position;
            Vector3 boxSize = new Vector3(SO.repositionBoxWidth, SO.repositionBoxHeight, 0);
            Gizmos.DrawWireCube(boxCenter, boxSize);
        }
    }
}

public class Ranged_IdleState : BaseState
{
    private RangedMonsterFSM _fsm;
    public Ranged_IdleState(RangedMonsterFSM fsm) : base(fsm) => _fsm = fsm;

    public override void Update()
    {
        // �÷��̾ Ž�� ���� �ȿ� ���Դٸ�
        if (_fsm.GetSqrDistanceToPlayer() <= _fsm.SO.detectionRange * _fsm.SO.detectionRange)
        {
            // ���� �ൿ(��ġ ����) ���·� ��ȯ
            _fsm.StateMachine.ChangeState(_fsm.RepositionState);
        }
    }
}

public class Ranged_RepositionState : BaseState
{
    private RangedMonsterFSM _fsm;
    public Ranged_RepositionState(RangedMonsterFSM fsm) : base(fsm) => _fsm = fsm;

    public override void Enter()
    {
        _fsm.Agent.isStopped = false;
        _fsm.FindAndSetNewWanderPosition(); 
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);

        // �������� ���� �����߰� ���� ��Ÿ���� �� �Ǿ��ٸ� ���� ���·� ��ȯ
        if (!_fsm.Agent.pathPending && _fsm.Agent.remainingDistance <= _fsm.Agent.stoppingDistance)
        {
            if (Time.time >= _fsm.lastAttackTime + _fsm.SO.attackCooldown)
            {
                _fsm.StateMachine.ChangeState(_fsm.AttackState);
            }
        }
    }
}
public class Ranged_AttackState : BaseState
{
    private RangedMonsterFSM _fsm;
    public Ranged_AttackState(RangedMonsterFSM fsm) : base(fsm) => _fsm = fsm;

    public override void Enter()
    {
        _fsm.Agent.isStopped = true;
        _fsm.Agent.ResetPath();

        _fsm.Owner.Flip(_fsm.Player); 

        FireNote();

        _fsm.lastAttackTime = Time.time;

        _fsm.StateMachine.ChangeState(_fsm.RepositionState);
    }

    private void FireNote()
    {
        if (_fsm.SO.notePrefab == null || _fsm.Player == null) return;

        GameObject note = Object.Instantiate(_fsm.SO.notePrefab, _fsm.transform.position, Quaternion.identity);
        NoteController noteController = note.GetComponent<NoteController>();

        Vector2 direction = (_fsm.Player.position - _fsm.transform.position).normalized;

        noteController.Initialize(direction, _fsm.SO.noteSpeed, _fsm.SO.attackPower);
    }
}
