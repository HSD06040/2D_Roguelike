using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState<T> where T : BossMonsterFSM
{
    public T fsm {  get; private set; }
    protected float timer;
    protected bool useTimer;

    private readonly int animHash;

    public BossBaseState(T _fsm, int _animHash)
    {
        fsm = _fsm;
        animHash = _animHash;
    }

    public virtual void Enter()
    {
        fsm.Owner.Animator.SetBool(animHash, true);
        fsm.animFinish = false;
    }

    public virtual void Exit() 
    {
        fsm.Owner.Animator.SetBool(animHash, false);
    }

    public virtual void Update()
    {
        if(useTimer && timer >= 0)
            timer -= Time.deltaTime;        
    }    
}
