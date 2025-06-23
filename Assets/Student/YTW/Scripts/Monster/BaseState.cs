using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    protected MonsterFSM _fsm;

    public BaseState(MonsterFSM fsm)
    {
        _fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
