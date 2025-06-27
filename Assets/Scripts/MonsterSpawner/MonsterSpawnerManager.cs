using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerManager : MonoBehaviour
{
    [Header("고정 소환위치")]
    [SerializeField] public Transform[] fixedSpawnPositions;

    [Header("랜덤 소환영역")]
    [SerializeField] public RectTransform[] randomSpawnAreas;

    [Header("랜던 소환영역별 몬스터 수")]
    [SerializeField] public int[] randomSpawnMonsterNum;

    [Header("고정 소환위치 몬스터목록")]
    [SerializeField] public GameObject[] fixedPosMonsterprefabs;

    [Header("랜덤 소환영역 몬스터목록")]
    [SerializeField] public GameObject[] randomePosMonsterprefabs;

    [Header("범위 태두리 여유공간")]
    [SerializeField] public float margin = 0.5f;

    private int spawnCount;
    public int SpawnCount => spawnCount;

    public void SpawnMonsters()
    {
        spawnCount = 0;

        SpawnFixedPosMonsters();

        for(int i = 0; i < randomSpawnAreas.Length; i++)
        {
            int spawnNum = randomSpawnMonsterNum[i]; // 인덱스에 넣은 소환할 몬스터 숫자
            spawnRandomPosMonsters(randomSpawnAreas[i], spawnNum); // 각각 영역별로 소환할 숫자전달
        }
        
        
        

    }

    private void SpawnFixedPosMonsters()  //고정위치 몬스터 소환
    {
        for (int i = 0; i < fixedSpawnPositions.Length; i++)
        {
            int random = Random.Range(0, fixedPosMonsterprefabs.Length);
            GameObject spawnMonster = fixedPosMonsterprefabs[random];

            Instantiate(spawnMonster, fixedSpawnPositions[i].position, Quaternion.identity);
            spawnCount++;
            Debug.Log("현재 몬스터 수: " + spawnCount);
        }
    }

    public void spawnRandomPosMonsters(RectTransform spawnArea, int spawnNum)  //랜덤위치 몬스터 소환
    {
        for (int i = 0; i < spawnNum; i++) //스폰 수만큼 반복
        {
            int random = Random.Range(0, randomePosMonsterprefabs.Length);
            GameObject spawnMonster = randomePosMonsterprefabs[random]; //랜덤하게 나온 숫자의 index에 해당하는 몬스터 프리팹

            Vector3 spawnPoint = RandomPosition(spawnArea); //스폰할 spawnArea와 스폰 포인트 설정
            Instantiate(spawnMonster, spawnPoint, Quaternion.identity); //몬스터 스폰
            spawnCount++;
            Debug.Log("현재 몬스터 수: " + spawnCount);
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
