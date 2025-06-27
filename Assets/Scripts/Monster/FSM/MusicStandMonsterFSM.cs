using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStandMonsterFSM : MonsterFSM
{
    [Header("��ȯ ����")]
    [SerializeField] private GameObject sheetMusicMonsterPrefab;
    [SerializeField] private Transform summonPoint;
    [SerializeField] private float summonCooldown = 10f;
    [Tooltip("��ȯ�� �� �ִ� ������ �ִ� ��")]
    [SerializeField] private int maxSummonedMonsters = 4; // �ִ� ��ȯ �� ���� ����

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

        // ���� ���Ͱ� �ִ��� Ȯ���ϰ� ����Ʈ���� ����
        // Linq�� RemoveAll�� ����Ͽ� ����Ʈ�� ��� �� null (�ı��� ���ӿ�����Ʈ)�� ���� ��� ����
        _musicStandFSM.SummonedMonsters.RemoveAll(monster => monster == null);

        _timer += Time.deltaTime;

        // ��Ÿ���� �Ǿ���, ���� ��ȯ�� ���� ���� �ִ�ġ���� ���� ���� ��ȯ ���·� ����
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
        // ���͸� ��ȯ�ϰ�, ��ȯ�� ������ ������ ����Ʈ�� �߰�
        GameObject newMonster = Object.Instantiate(_monsterToSummon, _summonPoint.position, _summonPoint.rotation);
        _musicStandFSM.SummonedMonsters.Add(newMonster);

        // ��ȯ �� ��� Idle ���·� ����
        __spreadFSM.StateMachine.ChangeState(_musicStandFSM.IdleState);
    }
}
