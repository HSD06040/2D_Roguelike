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

    [Header("파티클, 딜레이시간")]
    [SerializeField] private float monsterSpawnDelayTime = 2f;
    [SerializeField] private float ParticleDelayTime = 1f;
    [SerializeField] private GameObject spawnParticle;

    private int spawnCount;
    public int SpawnCount => spawnCount;

    public int SpawnMonsters()
    {
        spawnCount = 0;

        int totalCount = fixedSpawnPositions.Length;
        for (int i = 0; i < randomSpawnMonsterNum.Length; i++)
            totalCount += randomSpawnMonsterNum[i];

        SpawnFixedPosMonsters();

        for(int i = 0; i < randomSpawnAreas.Length; i++)
        {
            int spawnNum = randomSpawnMonsterNum[i]; // 인덱스에 넣은 소환할 몬스터 숫자
            SpawnRandomPosMonsters(randomSpawnAreas[i], spawnNum); // 각각 영역별로 소환할 숫자전달
        }
        return totalCount;
    }

    private void SpawnFixedPosMonsters()  //고정위치 몬스터 소환파티클
    {
        for (int i = 0; i < fixedSpawnPositions.Length; i++)
        {
            Vector3 spawnPoint = fixedSpawnPositions[i].position;
            StartCoroutine(SpawnFixedMonsterWithDelay(spawnPoint));
        }
    }

    private IEnumerator SpawnFixedMonsterWithDelay(Vector3 spawnPoint) //고정위치 몬스터 소환
    {
        yield return new WaitForSeconds(ParticleDelayTime);

        //GameObject particle = Manager.Resources.Instantiate(spawnParticle, spawnPoint, Quaternion.identity, true); //스폰 파티클 //////
        GameObject particle = Manager.Resources.Instantiate<GameObject>("MonsterSpawn/spawnParticle", spawnPoint, Quaternion.identity, true);
        Manager.Resources.Destroy(particle, monsterSpawnDelayTime);

        yield return new WaitForSeconds(monsterSpawnDelayTime);

        int random = Random.Range(0, fixedPosMonsterprefabs.Length);
        GameObject spawnMonster = fixedPosMonsterprefabs[random];

        Instantiate(spawnMonster, spawnPoint, Quaternion.identity);
        spawnCount++;
        Debug.Log("현재 몬스터 수: " + spawnCount);
    }


    public void SpawnRandomPosMonsters(RectTransform spawnArea, int spawnNum)  //몬스터 스폰위치에 파티클 생성
    {
        for (int i = 0; i < spawnNum; i++) //스폰 수만큼 반복
        {
            Vector3 spawnPoint = RandomPosition(spawnArea); //스폰할 spawnArea와 스폰 포인트 설정
            GameObject spawnMonster = Manager.Table.RandomMonsterSpawn(Manager.Game.currentChapter);
            StartCoroutine(SpawnMonsterWithDelay(spawnPoint));
            
        }
    }
    private IEnumerator SpawnMonsterWithDelay(Vector3 spawnPoint) //랜덤위치 몬스터 소환
    {
        yield return new WaitForSeconds(ParticleDelayTime);

        //GameObject particle = Manager.Resources.Instantiate(spawnParticle, spawnPoint, Quaternion.identity, true);
        GameObject particle = Manager.Resources.Instantiate<GameObject>("MonsterSpawn/spawnParticle", spawnPoint, Quaternion.identity, true); //스폰 파티클 //////
        Manager.Resources.Destroy(particle, monsterSpawnDelayTime + 0.5f);

        yield return new WaitForSeconds(monsterSpawnDelayTime);

        //랜덤하게 나온 숫자의 index에 해당하는 몬스터 프리팹
        GameObject spawnMonster = Manager.Table.RandomMonsterSpawn(Manager.Game.currentChapter); //몬스터 스폰
        Instantiate(spawnMonster, spawnPoint, Quaternion.identity);
        spawnCount++;
        Debug.Log("현재 몬스터 수: " + spawnCount);
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
