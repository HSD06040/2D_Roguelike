using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM_1 : BossMonsterFSM
{
    #region States
    public BossIdleState_1 idle { get; private set; }
    #endregion

    private void Awake()
    {
        idle = new BossIdleState_1(this, idleHash);
    }

    private void Start()
    {
        StateMachine.Initialize(idle);
    }
}
