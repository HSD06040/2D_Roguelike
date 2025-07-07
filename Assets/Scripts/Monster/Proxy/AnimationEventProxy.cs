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
        _monsterFSM?.AnimationAttackTrigger();
    }

    public void OnAttackEndEvent()
    {
        _monsterFSM?.OnAttackAnimationFinished();
    }

    public void OnDeathSound()
    {
        string soundPath = GetDeathSoundPath(_monsterFSM);
        Manager.Audio.PlaySFX(soundPath, _monsterFSM.transform.position);
        Debug.Log($"{_monsterFSM.name} 사운드 출력");
    }

    public void OnDeathAnimationEnd()
    {
        _monsterFSM?.DestroyMonster();
        Manager.Game.OnMonsterKill?.Invoke();
    }

    private string GetDeathSoundPath(MonsterFSM fsm)
    {
        if (fsm is ExplosionMonsterFSM explosionFSM)
        {
            return GetSoundPathFromType(explosionFSM.SO.deathSoundType);
        }
        else if (fsm is RangedShotMonsterFSM rangedFSM)
        {
            return GetSoundPathFromType(rangedFSM.SO.deathSoundType);
        }
        else if (fsm is SheetMusicMonsterFSM sheetFSM)
        {
            return GetSoundPathFromType(sheetFSM.SO.deathSoundType);
        }

        return "Monster/CreatuerDied"; 
    }

    private string GetSoundPathFromType(MonsterSoundType type)
    {
        switch (type)
        {
            case MonsterSoundType.CreatureDied:
                return "Monster/Died/CreatuerDied";
            case MonsterSoundType.HumanDied:
                return "Monster/Died/HumanDied";
            case MonsterSoundType.ExplosionMonsterDied:
                return "Monster/ExplosionMonster"; 
            case MonsterSoundType.SlimeDied:
                return "Monster/Died/SlimeDied";
            default:
                return "Monster/Died/CreatuerDied"; 
        }
    }
}
