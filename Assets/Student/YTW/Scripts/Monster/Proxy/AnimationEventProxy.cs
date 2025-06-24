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
            Debug.LogError("부모 오브젝트에서 IAnimationAttackHandler를 구현한 컴포넌트를 찾을 수 없습니다", gameObject);
        }
    }

    // 애니메이션 이벤트가 호출할 함수
    public void OnAttackEvent()
    {
        attackHandler?.AnimationAttackTrigger();
    }

    public void OnAttackEndEvent()
    {
        (attackHandler as SheetMusicMonsterFSM)?.OnAttackAnimationFinished();
    }
}
