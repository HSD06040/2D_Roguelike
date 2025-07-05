using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPattern : BossPattern
{
    [SerializeField] private Transform[] spawnPoints;
    protected override IEnumerator PatternRoutine()
    {
        yield return Utile.GetDelay(duration);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(prefab, spawnPoints[i].position, Quaternion.identity);
            
            for (int j = 0; j < Random.Range(2,4); j++)
            {
                Instantiate(Manager.Table.RandomMonsterSpawn(1), spawnPoints[i].position, Quaternion.identity);
            }
        }

        OnComplated?.Invoke();
    }
}
