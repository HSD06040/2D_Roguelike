using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedPositionMonsterSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPosition;
    [SerializeField] private GameObject[] monsterprefabs;

    public void RandomMonsterSpawn()
    {
        for (int i = 0; i < spawnPosition.Length; i++)
        {
            int random = Random.Range(0, monsterprefabs.Length);
            GameObject spawnMonster = monsterprefabs[random];

            Instantiate(spawnMonster, spawnPosition[i].position, Quaternion.identity);
        }
    }
}
