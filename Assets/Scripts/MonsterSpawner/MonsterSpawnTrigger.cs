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

    private int monsterLeft; // ���� ���� ��

    private void Start()
    {
        if(portal != null)
        {
            portal.SetActive(false);
        }

        doors.SetActive(false);
    }

    private void Update()  //�׽�Ʈ�� 
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
            Debug.Log("���Ͱ� �̹� ��ȯ�Ǿ��� ���Դϴ�.");
            return;
        }           
    }
    
    private IEnumerator MonsterSpawnStart()
    {
        yield return new WaitForSeconds(monsterSpawnDelayTime);
        monsterSpawnerManager = GetComponent<MonsterSpawnerManager>();
        monsterSpawnerManager.SpawnMonsters();

        monsterLeft = monsterSpawnerManager.SpawnCount; //���� ���� �� �Ŵ������� ������
        Debug.Log("��ȯ�� ���� ��:" + monsterLeft);
        
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
        Debug.Log("���� ���� ��:" + monsterLeft);

        if (monsterLeft <= 0)
        {
            Debug.Log("�� Ŭ����");
            isRoomCleared = true;
            UnlockDoors();

            if(isLastRoomOfStage)
            {
                portal.SetActive(true);
            }

        }
    }
}    

