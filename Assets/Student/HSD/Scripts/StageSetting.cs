using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSetting : MonoBehaviour
{
    [SerializeField] private int stageIdx;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Manager.Game.currentChapter = stageIdx;
            Destroy(gameObject);
        }
    }
}
