using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationUI_Base : MonoBehaviour
{
    private Animator anim;

    private static readonly int inHash = Animator.StringToHash("In");
    private static readonly int outHash = Animator.StringToHash("Out");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        anim.SetTrigger(inHash);
        anim.ResetTrigger(inHash);
    }

    public virtual void Close()
    {
        anim.SetTrigger(outHash);
    }

    public void SetActiveFalse() => gameObject.SetActive(false);
}
