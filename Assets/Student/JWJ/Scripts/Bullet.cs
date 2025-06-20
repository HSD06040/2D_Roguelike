using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerController player;

    private float damage;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    public void setDamage(float playerStrength)
    {
        damage = playerStrength;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.collider.CompareTag("Monster"))
       {
            DamageSystemExample target = collision.collider.GetComponent<DamageSystemExample>();
            player.Attack(target);
            Destroy(gameObject);

            Debug.Log(target.name + "에게" + damage + "의 데미지");
       }
        
    }
}
