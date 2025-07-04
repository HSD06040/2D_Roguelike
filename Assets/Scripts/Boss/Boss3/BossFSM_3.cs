using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossFSM_3 : BossMonsterFSM
{
    public BossPatternController_3 Pattern;

    public BossStateMachine<BossFSM_3> StateMachine;


    #region States
    public BossIdleState_3 Idle { get; private set; }
    public LinePatternState_3 Line { get; private set; }
    public CrossState_3 Cross { get; private set; }
    public GlissandoState_3 Glissando { get; private set; }
    public PianoBlackState_3 PianoBlack { get; private set; }
    public PianoWhiteState_3 PianoWhite { get; private set; }
    public BossDieState_3 die { get; private set; }
    #endregion

    private void Awake()
    {
        StateMachine = new BossStateMachine<BossFSM_3>();

        Idle = new BossIdleState_3(this, idleHash);
        Line = new LinePatternState_3(this, idleHash);
        Cross = new CrossState_3(this, idleHash);
        Glissando = new GlissandoState_3(this, idleHash);
        PianoWhite = new PianoWhiteState_3(this, idleHash);
        PianoBlack = new PianoBlackState_3(this, idleHash);
        die = new BossDieState_3(this, idleHash);

        Owner.OnDied += () => { StateMachine.ChangeState(die); };
    }

    private void Update()
    {
        StateMachine.Update();
    }

    public void Attack()
    {
        Vector2 dir = (Player.position - transform.position).normalized;

        Manager.Resources.Instantiate(Pattern.defaultAttackPrefab, transform.position, Quaternion.identity, true)
            .GetComponent<Projectile_Controller>().Initialize(dir, 4, 1, "");
    }
}

public class TobebenBaseState : BossBaseState<BossFSM_3>
{
    private static float staticTimer;

    public TobebenBaseState(BossFSM_3 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        staticTimer -= Time.deltaTime;

        if(staticTimer <= 0)
        {
            fsm.Attack();
        }
    }

    protected void ChangeIdleState() => fsm.StateMachine.ChangeState(fsm.Idle);
}

public class BossIdleState_3 : TobebenBaseState
{
    private bool canPattern;

    public BossIdleState_3(BossFSM_3 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        useTimer = true;
    }

    public override void Enter()
    {
        base.Enter();
        timer = 5;
        canPattern = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(timer >= 0 && canPattern)
        {
            switch(Random.Range(0, 5))
            {
                case 0: fsm.StateMachine.ChangeState(fsm.Cross); break;
                case 1: fsm.StateMachine.ChangeState(fsm.Line); break;
                case 2: fsm.StateMachine.ChangeState(fsm.Glissando); break;
                case 3: fsm.StateMachine.ChangeState(fsm.PianoBlack); break;
                case 4: fsm.StateMachine.ChangeState(fsm.PianoWhite); break;
            }
            canPattern = false;
        }
    }
}

public class GlissandoState_3 : TobebenBaseState
{
    public GlissandoState_3(BossFSM_3 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddGlissandoEvent(ChangeIdleState);
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Pattern.PlayGlissando();
    }
}

public class CrossState_3 : TobebenBaseState
{
    public CrossState_3(BossFSM_3 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddCrossEvent(ChangeIdleState);
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Pattern.PlayCross();
    }
}

public class PianoBlackState_3 : TobebenBaseState
{
    public PianoBlackState_3(BossFSM_3 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddPianoBlackEvent(ChangeIdleState);
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Pattern.PlayPianoBlack();
    }
}

public class PianoWhiteState_3 : TobebenBaseState
{
    public PianoWhiteState_3(BossFSM_3 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddPianoWhiteEvent(ChangeIdleState);
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Pattern.PlayPianoWhite();
    }
}

public class LinePatternState_3 : TobebenBaseState
{
    public LinePatternState_3(BossFSM_3 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddLineEvent(ChangeIdleState);
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Pattern.PlayLine();
    }
}

public class BossDieState_3 : BossBaseState<BossFSM_3>
{
    public BossDieState_3(BossFSM_3 _fsm, int _animHash) : base(_fsm, _animHash)
    {
    }

    public override void Enter()
    {
        base.Enter();

        fsm.Owner.DropCoin(fsm.stat);
        Object.Destroy(fsm.Owner.gameObject, 3);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}