using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionMonsterSpawner : MonoBehaviour
{
    [Header("스폰범위")]
    [SerializeField] private RectTransform[] spawnAreas;

    [Header("소환될 몬스터 목록")]
    [SerializeField] private GameObject[] monsterPrefabs;

    [Header("소환할 몬스터 수")]
    [SerializeField] private int spawnMonsterNum;

    [Header("범위 태두리 여유공간")]
    [SerializeField] private float space; //태두리 안쪽으로 여유공간

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            spawnRandomMonster();
        }
    }

    public void spawnRandomMonster()
    {
        for (int i = 0; i < spawnMonsterNum; i++)
        {
            RectTransform spawnArea = spawnAreas[Random.Range(0, spawnAreas.Length)]; //SpawnArea중 하나를 랜덤으로 뽑음
            int random = Random.Range(0, monsterPrefabs.Length);  //몬스터 index길이중 랜덤한 숫자를 뽑음
            GameObject spawnMonster = monsterPrefabs[random]; //랜덤하게 나온 숫자의 index에 해당하는 몬스터 프리팹

            Vector3 spawnPoint = RandomPosition(spawnArea); //스폰할 spawnArea와 스폰 포인트 설정
            Instantiate(spawnMonster, spawnPoint, Quaternion.identity); //몬스터 스폰
        } 
    }

    private Vector3 RandomPosition(RectTransform spawnArea)
    {
        Vector3[] rectCorners = new Vector3[4];  //0: 왼쪽아래, 1: 왼쪽 위, 2: 오른쪽 위, 3: 오른쪽 아래
        spawnArea.GetWorldCorners(rectCorners);

        float minX = rectCorners[0].x + space;
        float maxX = rectCorners[2].x - space;
        float minY = rectCorners[0].y + space;
        float maxY = rectCorners[2].y - space;

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        return new Vector3(randomX, randomY, 0);
    }

}
