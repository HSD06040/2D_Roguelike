using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossFSM_1 : BossMonsterFSM
{
    public BossPatternController_1 Pattern {  get; private set; }

    public BossStateMachine<BossFSM_1> StateMachine;

    #region States
    public BossIdleState_1 idle { get; private set; }
    public BossAttackState_1 attack { get; private set; }
    #endregion

    private void Awake()
    {
        Pattern = GetComponent<BossPatternController_1>();
        StateMachine = new BossStateMachine<BossFSM_1>();

        idle = new BossIdleState_1(this, idleHash);
        attack = new BossAttackState_1(this, attackHash);
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
        useTimer = true;
    }

    public override void Enter()
    {
        base.Enter();

        timer = Random.Range(3f, 6f);
    }

    public override void Update()
    {
        if(!fsm.isPatternPlaying && timer >= 0)
            timer -= Time.deltaTime;

        if(timer <= 0)
        {
            switch (Random.Range(0, 5))
            {
                case 0: fsm.StateMachine.ChangeState(fsm.attack); break;
            }
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
    private float teleportDelay = 4f;
    private bool hasTeleported = false;

    public BossTeleportState_1(BossFSM_1 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        useTimer = true;
    }

    public override void Enter()
    {
        base.Enter();

        hasTeleported = false;

        fsm.Player ??= GameObject.FindWithTag("Player").transform;

        fsm.StartCoroutine(FadeOutCoroutine());
    }

    public override void Update()
    {
        timer += Time.deltaTime;

        if (timer >= teleportDelay && !hasTeleported)
        {
            hasTeleported = true;

            fsm.Pattern.PlayCirclePattern();
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
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
    }
}

public class BossAttackState_1 : PatternState
{
    private float attackTime = 3f;

    public BossAttackState_1(BossFSM_1 _fsm, int _animHash) : base(_fsm, _animHash)
    {
        useTimer = true;
    }

    public override void Enter()
    {
        base.Enter();

        timer = attackTime;

        switch (Random.Range(0, 3))
        {
            case 0: fsm.Pattern.PlayCrossPattern(); break;
            case 1: fsm.Pattern.PlayXPattern(); break;
            case 2: fsm.Pattern.PlayCrossPattern(); break;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (timer <= 0)
            fsm.StateMachine.ChangeState(fsm.idle);
    }
}

