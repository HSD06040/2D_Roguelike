using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected Monster _monster;
    protected StateMachine _stateMachine;

    public State(Monster monster, StateMachine stateMachine)
    {
        _monster = monster;
        _stateMachine = stateMachine;
    }

    // 상태에 진입 시 한번 호출
    public virtual void Enter() { }

    // 상태가 활성화된 동안 매 프레임 호출
    public virtual void Execute() { }

    // 상태에서 빠져나갈 떄 한번 호출
    public virtual void Exit() { }
}
