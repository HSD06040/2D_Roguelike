using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 0)
        {
            //other.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
