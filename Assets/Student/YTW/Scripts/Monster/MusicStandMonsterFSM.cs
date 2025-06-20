using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStandMonsterFSM : MonsterFSM
{
    [Header("소환 설정")]
    [SerializeField] private GameObject sheetMusicMonsterPrefab;
    [SerializeField] private Transform summonPoint;
    [SerializeField] private float summonCooldown = 10f;

    public MusicStand_IdleState IdleState { get; private set; }
    public MusicStand_SummonState SummonState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        IdleState = new MusicStand_IdleState(this, summonCooldown);
        SummonState = new MusicStand_SummonState(this, sheetMusicMonsterPrefab, summonPoint);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }
}
public class MusicStand_IdleState : BaseState
{
    private float _summonCooldown;
    private float _timer;

    public MusicStand_IdleState(MusicStandMonsterFSM fsm, float summonCooldown) : base(fsm)
    {
        _summonCooldown = summonCooldown;
    }

    public override void Enter()
    {
        _timer = 0f;
    }

    public override void Update()
    {
        _fsm.Owner.Flip(_fsm.Player);

        _timer += Time.deltaTime;
        if (_timer >= _summonCooldown)
        {
            _fsm.StateMachine.ChangeState(((MusicStandMonsterFSM)_fsm).SummonState);
        }
    }
}
public class MusicStand_SummonState : BaseState
{
    private GameObject _monsterToSummon;
    private Transform _summonPoint;

    public MusicStand_SummonState(MusicStandMonsterFSM fsm, GameObject monsterPrefab, Transform summonPoint) : base(fsm)
    {
        _monsterToSummon = monsterPrefab;
        _summonPoint = summonPoint;
    }

    public override void Enter()
    {
        Object.Instantiate(_monsterToSummon, _summonPoint.position, _summonPoint.rotation);

        _fsm.StateMachine.ChangeState(((MusicStandMonsterFSM)_fsm).IdleState);
    }
}
