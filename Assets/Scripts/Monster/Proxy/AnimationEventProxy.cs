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
            Debug.LogError("부모 오브젝트에서 MonsterFSM을 구현한 컴포넌트를 찾을 수 없습니다", gameObject);
        }
    }

    // 애니메이션 이벤트가 호출할 함수
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
        // MonsterFSM에 있는 DestroyMonster 함수를 직접 호출
        _monsterFSM?.DestroyMonster();
    }
}
