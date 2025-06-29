using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : MonoBehaviour
{
    public BossBaseState CurState;

    public void Initialize(BossBaseState startingState)
    {
        CurState = startingState;
        CurState.Enter();
    }

    public void ChangeState(BossBaseState newState)
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
