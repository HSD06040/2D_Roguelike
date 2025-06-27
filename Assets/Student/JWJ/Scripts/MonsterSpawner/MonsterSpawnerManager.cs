using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerManager : MonoBehaviour
{
    [Header("���� ��ȯ��ġ")]
    [SerializeField] public Transform[] fixedSpawnPositions;

    [Header("���� ��ȯ����")]
    [SerializeField] public RectTransform[] randomSpawnAreas;

    [Header("���� ��ȯ������ ���� ��")]
    [SerializeField] public int[] randomSpawnMonsterNum;

    [Header("���� ��ȯ��ġ ���͸��")]
    [SerializeField] public GameObject[] fixedPosMonsterprefabs;

    [Header("���� ��ȯ���� ���͸��")]
    [SerializeField] public GameObject[] randomePosMonsterprefabs;

    [Header("���� �µθ� ��������")]
    [SerializeField] public float margin = 0.5f;

    private int spawnCount;
    public int SpawnCount => spawnCount;

    public void SpawnMonsters()
    {
        spawnCount = 0;

        SpawnFixedPosMonsters();

        for(int i = 0; i < randomSpawnAreas.Length; i++)
        {
            int spawnNum = randomSpawnMonsterNum[i]; // �ε����� ���� ��ȯ�� ���� ����
            spawnRandomPosMonsters(randomSpawnAreas[i], spawnNum); // ���� �������� ��ȯ�� ��������
        }
        
        
        

    }

    private void SpawnFixedPosMonsters()  //������ġ ���� ��ȯ
    {
        for (int i = 0; i < fixedSpawnPositions.Length; i++)
        {
            int random = Random.Range(0, fixedPosMonsterprefabs.Length);
            GameObject spawnMonster = fixedPosMonsterprefabs[random];

            Instantiate(spawnMonster, fixedSpawnPositions[i].position, Quaternion.identity);
            spawnCount++;
            Debug.Log("���� ���� ��: " + spawnCount);
        }
    }

    public void spawnRandomPosMonsters(RectTransform spawnArea, int spawnNum)  //������ġ ���� ��ȯ
    {
        for (int i = 0; i < spawnNum; i++) //���� ����ŭ �ݺ�
        {
            int random = Random.Range(0, randomePosMonsterprefabs.Length);
            GameObject spawnMonster = randomePosMonsterprefabs[random]; //�����ϰ� ���� ������ index�� �ش��ϴ� ���� ������

            Vector3 spawnPoint = RandomPosition(spawnArea); //������ spawnArea�� ���� ����Ʈ ����
            Instantiate(spawnMonster, spawnPoint, Quaternion.identity); //���� ����
            spawnCount++;
            Debug.Log("���� ���� ��: " + spawnCount);
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
