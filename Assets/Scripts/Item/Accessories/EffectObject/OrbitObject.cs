using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((1 << 6 & (1 << collision.gameObject.layer)) != 0)
        {
            collision.GetComponent<IDamagable>().TakeDamage(Manager.Data.PlayerStatus.Damage.Value);
        }
    }
}
