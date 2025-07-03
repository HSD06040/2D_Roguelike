using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossFSM_1 : BossMonsterFSM
{
    public BossPatternController_1 Pattern;

    public BossStateMachine<BossFSM_1> StateMachine;

    private static readonly int attack2Hash = Animator.StringToHash("Attack2");

    #region States
    public BossIdleState_1 idle { get; private set; }
    public BossEnemySpawnState_1 spawn { get; private set; }
    public BossDoubleCrossState_1 doubleCross { get; private set; }
    public BossTeleportState_1 telpo { get; private set; }
    public BossCrossState_1 cross { get; private set; }
    public BossDieState_1 die { get; private set; }
    #endregion

    private void Awake()
    {
        StateMachine = new BossStateMachine<BossFSM_1>();

        idle = new BossIdleState_1(this, idleHash);
        spawn = new BossEnemySpawnState_1(this, attackHash);
        doubleCross = new BossDoubleCrossState_1(this, attack2Hash);
        telpo = new BossTeleportState_1(this, idleHash);
        cross  = new BossCrossState_1(this, idleHash);
        die = new BossDieState_1(this, dieHash);

        Owner.OnDied += () => { StateMachine.ChangeState(die); };
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void Start()
    {
        StateMachine.Initialize(idle);
    } 
}
public class BossIdleState_1 : BossBaseState<BossFSM_1>
{
    public BossIdleState_1(BossFSM_1 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddEventDefault(ChangeState);
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Pattern.PlayDefaultPattern();
    }

    private void ChangeState()
    {
        switch (Random.Range(0, 4))
        {
            case 0: fsm.StateMachine.ChangeState(fsm.telpo); break;
            case 1: fsm.StateMachine.ChangeState(fsm.spawn); break;
            case 2: fsm.StateMachine.ChangeState(fsm.cross); break;
            case 3: fsm.StateMachine.ChangeState(fsm.doubleCross); break;
        }
    }
}

public class PatternState : BossBaseState<BossFSM_1>
{
    public PatternState(BossFSM_1 _fsm, int _animHash) : base(_fsm, _animHash)
    {
    }

    public override void Enter()
    {
        base.Enter();

        fsm.isPatternPlaying = true;
    }
    public override void Exit()
    {
        base.Exit();

        fsm.isPatternPlaying = false;
    }
}

public class BossTeleportState_1 : PatternState
{
    private float fadeDuration = 1f;

    public BossTeleportState_1(BossFSM_1 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddEventCircle(ChangeState);
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Owner.Invincible = true;
        fsm.StartCoroutine(FadeOutCoroutine());
    }

    private void ChangeState() => fsm.StateMachine.ChangeState(fsm.idle);

    private IEnumerator FadeOutCoroutine()
    {
        fsm.Pattern.PlayCirclePattern();
        float elapsed = 0f;
        Color originalColor = fsm.Owner.spriteRenderer.color;

        while (elapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            fsm.Owner.spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        fsm.Owner.spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    public override void Exit()
    {
        base.Exit();

        if (fsm.Owner.spriteRenderer != null)
        {
            Color c = fsm.Owner.spriteRenderer.color;
            fsm.Owner.spriteRenderer.color = new Color(c.r, c.g, c.b, 1f);
        }
        fsm.Owner.Invincible = false;
    }
}

public class BossEnemySpawnState_1 : PatternState
{    
    public BossEnemySpawnState_1(BossFSM_1 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddEventEnemySpawn(ChangeState);
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Pattern.PlayEnemySpawnPattern();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void ChangeState() => fsm.StateMachine.ChangeState(fsm.idle);
}

public class BossCrossState_1 : BossBaseState<BossFSM_1>
{
    public BossCrossState_1(BossFSM_1 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddEventCross(ChangeState);
    }

    public override void Enter()
    {
        base.Enter();
        fsm.Pattern.PlayCrossRotatePattern();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void ChangeState() => fsm.StateMachine.ChangeState(fsm.idle);
}

public class BossDoubleCrossState_1 : PatternState
{
    public BossDoubleCrossState_1(BossFSM_1 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        fsm.Pattern.AddEventDoubleCross(ChangeState);
    }

    public override void Enter()
    {
        base.Enter();

        fsm.Pattern.PlayDoubleCross();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void ChangeState() => fsm.StateMachine.ChangeState(fsm.idle);
}

public class BossDieState_1 : BossBaseState<BossFSM_1>
{
    public BossDieState_1(BossFSM_1 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        
    }

    public override void Enter()
    {
        base.Enter();

        fsm.Owner.DropCoin(fsm.stat);
        fsm.StartDieRoutine();       
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

