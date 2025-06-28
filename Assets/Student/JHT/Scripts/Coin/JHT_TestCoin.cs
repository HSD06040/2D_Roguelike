using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHT_TestCoin : MonoBehaviour
{
    public int CoinNum;
    

    private int rand;

    private PlayerController player;

    private void Start()
    {
        rand = Random.Range(10, 100);
        CoinNum = rand;

        player = FindObjectOfType<PlayerController>();
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // 코인추가;
        }
    }
}
