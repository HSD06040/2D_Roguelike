using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerManager : MonoBehaviour
{
    [Header("�� ��ȯ�� ���� ��")]
    [SerializeField] private int spawnMonsterNum;

    [Header("���� ��ȯ��ġ")]
    [SerializeField] public Transform[] fixedSpawnPositions;

    [Header("���� ��ȯ��ġ ���͸��")]
    [SerializeField] public GameObject[] fixedPosMonsterprefabs;

    [Header("���� ��ȯ����")]
    [SerializeField] public RectTransform[] randomSpawnAreas;

    [Header("���� ��ȯ���� ���͸��")]
    [SerializeField] public GameObject[] randomePosMonsterprefabs;

    [Header("���� �µθ� ��������")]
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
            Debug.Log("��ȯ�� �� ������ �� ���� ���� ��ġ�� ���� �����ϴ�.");
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
            RectTransform spawnArea = randomSpawnAreas[Random.Range(0, randomSpawnAreas.Length)]; //SpawnArea�� �ϳ��� �������� ����
            int random = Random.Range(0, randomePosMonsterprefabs.Length);  //���� index������ ������ ���ڸ� ����
            GameObject spawnMonster = randomePosMonsterprefabs[random]; //�����ϰ� ���� ������ index�� �ش��ϴ� ���� ������

            Vector3 spawnPoint = RandomPosition(spawnArea); //������ spawnArea�� ���� ����Ʈ ����
            Instantiate(spawnMonster, spawnPoint, Quaternion.identity); //���� ����
        }
    }

    private Vector3 RandomPosition(RectTransform spawnArea)
    {
        Vector3[] rectCorners = new Vector3[4];  //0: ���ʾƷ�, 1: ���� ��, 2: ������ ��, 3: ������ �Ʒ�
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
