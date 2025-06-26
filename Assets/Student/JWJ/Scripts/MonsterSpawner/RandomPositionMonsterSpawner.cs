using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionMonsterSpawner : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] private RectTransform[] spawnAreas;

    [Header("��ȯ�� ���� ���")]
    [SerializeField] private GameObject[] monsterPrefabs;

    [Header("��ȯ�� ���� ��")]
    [SerializeField] private int spawnMonsterNum;

    [Header("���� �µθ� ��������")]
    [SerializeField] private float space; //�µθ� �������� ��������

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
            RectTransform spawnArea = spawnAreas[Random.Range(0, spawnAreas.Length)]; //SpawnArea�� �ϳ��� �������� ����
            int random = Random.Range(0, monsterPrefabs.Length);  //���� index������ ������ ���ڸ� ����
            GameObject spawnMonster = monsterPrefabs[random]; //�����ϰ� ���� ������ index�� �ش��ϴ� ���� ������

            Vector3 spawnPoint = RandomPosition(spawnArea); //������ spawnArea�� ���� ����Ʈ ����
            Instantiate(spawnMonster, spawnPoint, Quaternion.identity); //���� ����
        } 
    }

    private Vector3 RandomPosition(RectTransform spawnArea)
    {
        Vector3[] rectCorners = new Vector3[4];  //0: ���ʾƷ�, 1: ���� ��, 2: ������ ��, 3: ������ �Ʒ�
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
