using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float damage;
    public void setDamage(float damageAmount)
    {
        damage = damageAmount;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       StatusController target = collision.gameObject.GetComponent<StatusController>();
        if(target != null)
        {
            target.TakeDamage(damage);
            Debug.Log(target.gameObject.name + "에게" + damage + "대미지 줌");
            Destroy(gameObject);
        }
    }
}
