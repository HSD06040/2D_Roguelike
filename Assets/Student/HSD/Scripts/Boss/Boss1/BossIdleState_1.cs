using UnityEngine;

public class BossIdleState_1 : BossBaseState
{
    public BossIdleState_1(BossMonsterFSM _fsm, int _animHash) : base(_fsm, _animHash)
    {
        useTimer = true;
    }

    public override void Update()
    {
        base.Update();
    }
}

public class BossChaseState_1 : BossBaseState
{
    private float keepTime = 1f;
    private float moveTime = 0.5f;
    private float speed = 5f;

    private Transform target;
    private Vector2 direction;
    private Vector2 startPosition;
    private Vector2 endPosition;

    private float elapsed = 0f;

    private enum Phase
    {
        Watching,
        Moving,
        Done
    }

    private Phase phase = Phase.Watching;

    public BossChaseState_1(BossMonsterFSM _fsm, int _animHash) : base(_fsm, _animHash)
    {
    }

    public override void Enter()
    {
        base.Enter();
        target = fsm.Player;
        elapsed = 0f;
        phase = Phase.Watching;
    }

    public override void Update()
    {
        base.Update();

        switch (phase)
        {
            case Phase.Watching:
                elapsed += Time.deltaTime;
                fsm.Owner.transform.LookAt(target.position);

                if (elapsed >= keepTime)
                {
                    direction = (target.position - fsm.Owner.transform.position).normalized;
                    startPosition = fsm.Owner.transform.position;
                    endPosition = startPosition + direction * speed * moveTime;

                    elapsed = 0f;
                    phase = Phase.Moving;
                }
                break;

            case Phase.Moving:
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / moveTime);
                fsm.Owner.transform.position = Vector2.Lerp(startPosition, endPosition, t);

                if (t >= 1f)
                {
                    phase = Phase.Done;

                    //fsm.StateMachine.ChangeState(fsm.AttackState_2);
                }
                break;

            case Phase.Done:
                break;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
