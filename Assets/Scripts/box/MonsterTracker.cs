using UnityEngine;

public class MonsterTracker : MonoBehaviour //몬스터 확인후 상자 획득 기능
{
    public GameObject[] monsters;         // 추적할 몬스터들
    public GameObject chestToActivate;    // 상자 오브젝트 (초기엔 비활성화되어 있음)

    void Start()
    {
        chestToActivate.SetActive(false); // 시작 시 상자 숨김
    }

    void Update()
    {
        // 모든 몬스터가 죽었는지 확인
        bool allDead = true;
        foreach (GameObject monster in monsters)
        {
            if (monster != null) // 살아 있는 몬스터가 있다면
            {
                allDead = false;
                break;
            }
        }

        // 모두 죽었으면 상자 활성화
        if (allDead && !chestToActivate.activeSelf)
        {
            Debug.Log("모든 몬스터를 처치했습니다. 상자가 나타납니다!");
            chestToActivate.SetActive(true);
        }
    }
}
