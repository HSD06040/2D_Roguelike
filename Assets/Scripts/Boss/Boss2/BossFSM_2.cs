using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM_2 : BossMonsterFSM
{
    public BossPatternController_2 Pattern;

    public BossStateMachine<BossFSM_2> StateMachine;

    #region States
    public BossIdleState_2 idle {  get; private set; }
    public BossTeleportState_2 telpo {  get; private set; }
    public BossDoubleShotState_2 doubleShot { get; private set; }
    public BossExplosionPatternState_2 explosion { get; private set; }
    public BossCrossState_2 cross { get; private set; }
    public BossCrossRotateState_2 crossRotate { get; private set; }
    public BossLaserState_2 laser { get; private set; }
    public BossDieState_2 die {  get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new BossStateMachine<BossFSM_2>();

        idle = new BossIdleState_2(this, idleHash);
        telpo = new BossTeleportState_2(this, moveHash);
        doubleShot = new BossDoubleShotState_2(this, attackHash);
        explosion = new BossExplosionPatternState_2(this, attackHash);
        cross = new BossCrossState_2(this, attackHash);
        crossRotate = new BossCrossRotateState_2(this, attackHash);
        laser = new BossLaserState_2(this, attackHash);
        die = new BossDieState_2(this, dieHash);
    }

    private void Update()
    {
        StateMachine.Update();
    }

    protected override void Start()
    {
        base.Start();
    }

    public void ChangeIdleState() => StateMachine.ChangeState(idle);
}

public class BossIdleState_2 : BossBaseState<BossFSM_2>
{    
    public BossIdleState_2(BossFSM_2 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        useTimer = true;
    }

    public override void Enter()
    {
        base.Enter();
        timer = 4;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (timer < 0)
            fsm.StateMachine.ChangeState(fsm.telpo);
    }
}

public class BossTeleportState_2 : BossBaseState<BossFSM_2>
{
    private bool beforeCenter;

    public BossTeleportState_2(BossFSM_2 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddCenterTeleportEvent(ChangeState);
        fsm.Pattern.AddSideTeleportEvent(ChangeState);
    }

    public override void Enter()
    {
        base.Enter();

        if(beforeCenter)
        {
            fsm.Pattern.PlaySideTeleportPattern();
        }
        else
        {
            fsm.Pattern.PlayCenterTeleportPattern();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    private void ChangeState()
    {
        if(beforeCenter)
        {
            switch (Random.Range(0, 2))
            {
                case 0: fsm.StateMachine.ChangeState(fsm.cross); break;
                case 1: fsm.StateMachine.ChangeState(fsm.crossRotate); break;
            }
        }
        else
        {
            switch (Random.Range(0, 3))
            {
                case 0: fsm.StateMachine.ChangeState(fsm.explosion); break;
                case 1: fsm.StateMachine.ChangeState(fsm.laser); break;
                case 2: fsm.StateMachine.ChangeState(fsm.doubleShot); break;
            }
        }
    }
}

public class BossCrossState_2 : BossBaseState<BossFSM_2>
{
    public BossCrossState_2(BossFSM_2 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddCrossEvent(fsm.ChangeIdleState);
    }

    public override void Enter()
    {
        base.Enter();

        fsm.Pattern.PlayLinePattern();
        fsm.Pattern.PlayCrossPattern();
    }
}

public class BossCrossRotateState_2 : BossBaseState<BossFSM_2>
{
    public BossCrossRotateState_2(BossFSM_2 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddCrossRotateEvent(fsm.ChangeIdleState);
    }

    public override void Enter()
    {
        base.Enter();

        fsm.Pattern.PlayLinePattern();
        fsm.Pattern.PlayCrossRotatePattern();
    }
}

public class BossLaserState_2 : BossBaseState<BossFSM_2>
{
    public BossLaserState_2(BossFSM_2 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddLaserEvent(fsm.ChangeIdleState);
    }

    public override void Enter()
    {
        base.Enter();

        fsm.Pattern.PlayLaserPattern();
    }
}

public class BossExplosionPatternState_2 : BossBaseState<BossFSM_2>
{
    public BossExplosionPatternState_2(BossFSM_2 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddExplosionEvent(fsm.ChangeIdleState);
    }

    public override void Enter()
    {
        base.Enter();

        fsm.Pattern.PlayExplosionPattern();
    }
}

public class BossDoubleShotState_2 : BossBaseState<BossFSM_2>
{
    public BossDoubleShotState_2(BossFSM_2 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddDoubleShotEvent(fsm.ChangeIdleState);
    }

    public override void Enter()
    {
        base.Enter();

        fsm.Pattern.PlayDoubleShotPattern();
    }
}

public class BossDieState_2 : BossBaseState<BossFSM_2>
{
    private bool isDead;

    public BossDieState_2(BossFSM_2 _fsm, int _animHash) : base(_fsm, _animHash)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (!isDead)
        {
            isDead = true;
            fsm.Pattern.CurrentBossPatternStop();
            fsm.Owner.DropCoin(fsm.stat);
            fsm.StartDieRoutine();
        }
    }
}