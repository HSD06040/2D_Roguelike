using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState
{
    public BossMonsterFSM fsm {  get; private set; }
    protected bool useTimer;
    protected float timer;
    private readonly int animHash;

    public BossBaseState(BossMonsterFSM _fsm, int _animHash)
    {
        fsm = _fsm;
        animHash = _animHash;
    }

    public virtual void Enter()
    {
        fsm.Owner.Animator.SetTrigger(animHash);
    }

    public virtual void Exit() 
    {
        fsm.Owner.Animator.ResetTrigger(animHash);
    }

    public virtual void Update()
    {
        if(useTimer && timer >= 0)
            timer -= Time.deltaTime;
    }
}
