using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    private IAnimationAttackHandler attackHandler;

    private void Awake()
    {
        attackHandler = GetComponentInParent<IAnimationAttackHandler>();
        if (attackHandler == null)
        {
            Debug.LogError("�θ� ������Ʈ���� IAnimationAttackHandler�� ������ ������Ʈ�� ã�� �� �����ϴ�", gameObject);
        }
    }

    // �ִϸ��̼� �̺�Ʈ�� ȣ���� �Լ�
    public void OnAttackEvent()
    {
        attackHandler?.AnimationAttackTrigger();
    }

    public void OnAttackEndEvent()
    {
        (attackHandler as SheetMusicMonsterFSM)?.OnAttackAnimationFinished();
    }
}
