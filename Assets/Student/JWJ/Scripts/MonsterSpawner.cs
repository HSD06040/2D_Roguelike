using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform spawnArea;
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private int spawnMonsterNum;
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
            int random = Random.Range(0, monsterPrefabs.Length);
            GameObject spawnMonster = monsterPrefabs[random];

            Vector3 spawnPoint = RandomPosition();
            Instantiate(spawnMonster, spawnPoint, Quaternion.identity);
        } 
    }

    private Vector3 RandomPosition()
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
