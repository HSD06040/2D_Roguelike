using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerManager : MonoBehaviour
{
    [Header("총 소환할 몬스터 수")]
    [SerializeField] private int spawnMonsterNum;

    [Header("고정 소환위치")]
    [SerializeField] public Transform[] fixedSpawnPositions;

    [Header("고정 소환위치 몬스터목록")]
    [SerializeField] public GameObject[] fixedPosMonsterprefabs;

    [Header("랜덤 소환영역")]
    [SerializeField] public RectTransform[] randomSpawnAreas;

    [Header("랜덤 소환영역 몬스터목록")]
    [SerializeField] public GameObject[] randomePosMonsterprefabs;

    [Header("범위 태두리 여유공간")]
    [SerializeField] public float margin = 0.5f;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            SpawnMonsters();
        }
    }

    private void SpawnMonsters()
    {
        int randomPosMonsterNum = spawnMonsterNum - fixedSpawnPositions.Length;

        if (randomPosMonsterNum < 0)
        {
            Debug.Log("소환할 총 몬스터의 수 보다 고정 위치의 수가 많습니다.");
            return;
        }
        else 
        {
            SpawnFixedPosMonsters();
            spawnRandomPosMonsters(randomPosMonsterNum);
        }
        

    }

    private void SpawnFixedPosMonsters()
    {
        for (int i = 0; i < fixedSpawnPositions.Length; i++)
        {
            int random = Random.Range(0, fixedPosMonsterprefabs.Length);
            GameObject spawnMonster = fixedPosMonsterprefabs[random];

            Instantiate(spawnMonster, fixedSpawnPositions[i].position, Quaternion.identity);
        }
    }

    public void spawnRandomPosMonsters(int randomPosMonsterNum)
    {
        for (int i = 0; i < randomPosMonsterNum; i++)
        {
            RectTransform spawnArea = randomSpawnAreas[Random.Range(0, randomSpawnAreas.Length)]; //SpawnArea중 하나를 랜덤으로 뽑음
            int random = Random.Range(0, randomePosMonsterprefabs.Length);  //몬스터 index길이중 랜덤한 숫자를 뽑음
            GameObject spawnMonster = randomePosMonsterprefabs[random]; //랜덤하게 나온 숫자의 index에 해당하는 몬스터 프리팹

            Vector3 spawnPoint = RandomPosition(spawnArea); //스폰할 spawnArea와 스폰 포인트 설정
            Instantiate(spawnMonster, spawnPoint, Quaternion.identity); //몬스터 스폰
        }
    }

    private Vector3 RandomPosition(RectTransform spawnArea)
    {
        Vector3[] rectCorners = new Vector3[4];  //0: 왼쪽아래, 1: 왼쪽 위, 2: 오른쪽 위, 3: 오른쪽 아래
        spawnArea.GetWorldCorners(rectCorners);

        float minX = rectCorners[0].x + margin;
        float maxX = rectCorners[2].x - margin;
        float minY = rectCorners[0].y + margin;
        float maxY = rectCorners[2].y - margin;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector3(randomX, randomY, 0);
    }
}
