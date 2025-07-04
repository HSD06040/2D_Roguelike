using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM_2 : BossMonsterFSM
{
    public BossPatternController_2 Pattern;

    public BossStateMachine<BossFSM_2> StateMachine;

    #region States
    public BossIdleState_1 idle { get; private set; }
    public BossEnemySpawnState_1 spawn { get; private set; }
    public BossDoubleCrossState_1 doubleCross { get; private set; }
    public BossTeleportState_1 telpo { get; private set; }
    public BossCrossState_1 cross { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new BossStateMachine<BossFSM_2>();
    }

    private void Update()
    {
        StateMachine.Update();
    }

    protected override void Start()
    {
        base.Start();
    }
}
