using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterFSM : MonoBehaviour
{
    public Transform Player;
    public Monster Owner;
    public BossStateMachine StateMachine;
    public BossPatternController BossPattern;

    #region AnimHash
    protected static readonly int idleHash = Animator.StringToHash("Idle");
    protected static readonly int attackHash = Animator.StringToHash("Attack");
    protected static readonly int runHash = Animator.StringToHash("Run");
    protected static readonly int deadHash = Animator.StringToHash("Dead");
    #endregion
    

    private void Update()
    {
        StateMachine.Update();
    }
}
