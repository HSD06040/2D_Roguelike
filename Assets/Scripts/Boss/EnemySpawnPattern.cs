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
            //몬스터 소환로직
        }

        OnComplated?.Invoke();
    }
}
