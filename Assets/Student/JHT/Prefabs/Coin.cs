using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int CoinNum;
    [SerializeField] private float coinSpeed;

    public void Init(int amount)
    {
        CoinNum = amount;
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        if(Manager.Data.PassiveCon.orbitController != null)
            transform.position = Vector2.MoveTowards(transform.position, 
                Manager.Data.PassiveCon.orbitController.transform.position, coinSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Manager.Data.AddGold(CoinNum);
            Destroy(gameObject);
        }

    }
}
