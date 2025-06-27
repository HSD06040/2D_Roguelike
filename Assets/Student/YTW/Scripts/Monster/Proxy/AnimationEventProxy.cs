using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventProxy : MonoBehaviour
{
    private MonsterFSM _monsterFSM;

    private void Awake()
    {
        _monsterFSM = GetComponentInParent<MonsterFSM>();
        if (_monsterFSM == null)
        {
            Debug.LogError("�θ� ������Ʈ���� MonsterFSM�� ������ ������Ʈ�� ã�� �� �����ϴ�", gameObject);
        }
    }

    // �ִϸ��̼� �̺�Ʈ�� ȣ���� �Լ�
    public void OnAttackEvent()
    {
        (_monsterFSM as IAnimationAttackHandler)?.AnimationAttackTrigger();
    }

    public void OnAttackEndEvent()
    {
        (_monsterFSM as IAnimationAttackHandler)?.OnAttackAnimationFinished();
    }
    public void OnDeathAnimationEnd()
    {
        // MonsterFSM�� �ִ� DestroyMonster �Լ��� ���� ȣ��
        _monsterFSM?.DestroyMonster();
    }
}
