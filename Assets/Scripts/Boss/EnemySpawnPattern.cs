using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPattern : BossPattern
{
    [SerializeField] private Transform[] spawnPoints;
    protected override IEnumerator PatternRoutine(Monster boss)
    {
        yield return Utile.GetDelay(duration);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(prefab, spawnPoints[i].position, Quaternion.identity);
            //���� ��ȯ����
        }

        OnComplated?.Invoke();
    }
}
