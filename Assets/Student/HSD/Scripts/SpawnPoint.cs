using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        player.position = transform.position;
    }
}
