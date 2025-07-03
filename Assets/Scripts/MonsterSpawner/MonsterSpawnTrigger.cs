using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterSpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject doors;
    [SerializeField] private GameObject portal;
    [SerializeField] private bool isLastRoomOfStage = false;

    [SerializeField] private float monsterSpawnDelayTime = 2f;
    

    private MonsterSpawnerManager monsterSpawnerManager;

    private bool hasMonsterSpawned = false;
    private bool isRoomCleared = false;

    private int monsterLeft; // 남은 몬스터 수

    private void Start()
    {
        if(portal != null)
        {
            portal.SetActive(false);
        }

        doors.SetActive(false);
    }

    private void Update()  //테스트용 
   {
       if(Input.GetKeyDown(KeyCode.K) && hasMonsterSpawned && !isRoomCleared)
       {
           MonsterDied();
       }    
   }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        { 
            return; 
        }
        if (!hasMonsterSpawned && !isRoomCleared)
        {
            Manager.Game.OnMonsterKill += MonsterDied;
            hasMonsterSpawned = true;
            LockDoors();
            StartCoroutine(MonsterSpawnStart());
        }
        else
        {
            Debug.Log("몬스터가 이미 소환되었던 방입니다.");
            return;
        }           
    }
    
    private IEnumerator MonsterSpawnStart()
    {
        yield return new WaitForSeconds(monsterSpawnDelayTime);
        monsterSpawnerManager = GetComponent<MonsterSpawnerManager>();
        monsterSpawnerManager.SpawnMonsters();

        monsterLeft = monsterSpawnerManager.SpawnCount; //남은 몬스터 수 매니져에서 가져옴
        Debug.Log("소환된 몬스터 수:" + monsterLeft);
        
    }
    private void LockDoors()
    {
        doors.SetActive(true);
    }

    private void UnlockDoors()
    {
        doors.SetActive(false);         
    }

    public void MonsterDied()
    {
        monsterLeft--;
        Debug.Log("남은 몬스터 수:" + monsterLeft);

        if (monsterLeft <= 0)
        {
            Debug.Log("방 클리어");
            isRoomCleared = true;
            UnlockDoors();

            if(isLastRoomOfStage)
            {
                portal.SetActive(true);
            }

        }
    }
}    

