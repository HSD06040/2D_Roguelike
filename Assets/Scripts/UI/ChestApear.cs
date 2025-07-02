using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestApear : MonoBehaviour
{
    [SerializeField] private GameObject chest;
    [SerializeField] private float boxPopUpDelayTime;
    private bool hasChestApeared = false;

    private void Start()
    {
        chest.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !hasChestApeared)
        {
            Debug.Log("플레이어충돌");
            hasChestApeared = true;
            StartCoroutine(ChestPopUpDelayTime());
        }
    }

    private IEnumerator ChestPopUpDelayTime()
    {
        yield return new WaitForSeconds(boxPopUpDelayTime);
        chest.SetActive(true);
    }
}
