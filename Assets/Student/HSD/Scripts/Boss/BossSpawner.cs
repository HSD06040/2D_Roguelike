using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float delay;
    private bool isSpawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isSpawn)
        {
            isSpawn = true;
            StartCoroutine(BossSpawnRoutine());
        }
    }

    private IEnumerator BossSpawnRoutine()
    {
        yield return Utile.GetDelay(delay);

        Instantiate(boss, spawnPoint.position, Quaternion.identity);
    }
}
