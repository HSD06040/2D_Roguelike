using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStandMonsterFSM : MonsterFSM
{
    [Header("소환 설정")]
    [SerializeField] private GameObject sheetMusicMonsterPrefab;
    [SerializeField] private Transform summonPoint;
    [SerializeField] private float summonCooldown = 10f;
    [Tooltip("소환할 수 있는 몬스터의 최대 수")]
    [SerializeField] private int maxSummonedMonsters = 4; // 최대 소환 수 설정 변수

    public List<GameObject> SummonedMonsters { get; private set; }

    public MusicStand_IdleState IdleState { get; private set; }
    public MusicStand_SummonState SummonState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SummonedMonsters = new List<GameObject>();

        IdleState = new MusicStand_IdleState(this, summonCooldown, maxSummonedMonsters);
        SummonState = new MusicStand_SummonState(this, sheetMusicMonsterPrefab, summonPoint);
    }

    private void Start()
    {
        StateMachine.Initialize(IdleState);
    }
}
public class MusicStand_IdleState : BaseState
{
    private MusicStandMonsterFSM _musicStandFSM;
    private float _summonCooldown;
    private int _maxSummonedMonsters;
    private float _timer;

    public MusicStand_IdleState(MusicStandMonsterFSM fsm, float summonCooldown, int maxSummonedMonsters) : base(fsm)
    {
        _musicStandFSM = fsm;
        _summonCooldown = summonCooldown;
        _maxSummonedMonsters = maxSummonedMonsters;
    }

    public override void Enter()
    {
        _timer = 0f;
    }

    public override void Update()
    {
        __spreadFSM.Owner.Flip(__spreadFSM.Player);

        // 죽은 몬스터가 있는지 확인하고 리스트에서 제거
        // Linq의 RemoveAll을 사용하여 리스트의 요소 중 null (파괴된 게임오브젝트)인 것을 모두 제거
        _musicStandFSM.SummonedMonsters.RemoveAll(monster => monster == null);

        _timer += Time.deltaTime;

        // 쿨타임이 되었고, 현재 소환된 몬스터 수가 최대치보다 적을 때만 소환 상태로 변경
        if (_timer >= _summonCooldown && _musicStandFSM.SummonedMonsters.Count < _maxSummonedMonsters)
        {
            __spreadFSM.StateMachine.ChangeState(_musicStandFSM.SummonState);
        }
    }
}
public class MusicStand_SummonState : BaseState
{
    private MusicStandMonsterFSM _musicStandFSM;
    private GameObject _monsterToSummon;
    private Transform _summonPoint;

    public MusicStand_SummonState(MusicStandMonsterFSM fsm, GameObject monsterPrefab, Transform summonPoint) : base(fsm)
    {
        _musicStandFSM = fsm;
        _monsterToSummon = monsterPrefab;
        _summonPoint = summonPoint;
    }

    public override void Enter()
    {
        // 몬스터를 소환하고, 소환된 몬스터의 정보를 리스트에 추가
        GameObject newMonster = Object.Instantiate(_monsterToSummon, _summonPoint.position, _summonPoint.rotation);
        _musicStandFSM.SummonedMonsters.Add(newMonster);

        // 소환 후 즉시 Idle 상태로 복귀
        __spreadFSM.StateMachine.ChangeState(_musicStandFSM.IdleState);
    }
}
