using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine<T> where T : BossMonsterFSM
{
    public BossBaseState<T> CurState;

    public void Initialize(BossBaseState<T> startingState)
    {
        CurState = startingState;
        CurState.Enter();
    }

    public void ChangeState(BossBaseState<T> newState)
    {
        CurState?.Exit();
        CurState = newState;
        CurState.Enter();
    }

    public void Update()
    {
        CurState?.Update();
    }
}
