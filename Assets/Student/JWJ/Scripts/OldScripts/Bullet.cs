using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private int damage;
    public void setDamage(int damageAmount)
    {
        damage = damageAmount;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       StatusController target = collision.gameObject.GetComponent<StatusController>();
        if(target != null)
        {
            target.TakeDamage(damage);
            Debug.Log(target.gameObject.name + "����" + damage + "����� ��");
            Destroy(gameObject);
        }
    }
}
