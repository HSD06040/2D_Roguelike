using System.Collections;
using System.Collections.Generic;
using UnityEditor.Connect;
using UnityEngine;

public class DamageSystemExample : MonoBehaviour
{
    [SerializeField] protected float maxHP;
    private float currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHP -= amount;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    public virtual void GiveDamage(DamageSystemExample target, float amount)
    {
        target.TakeDamage(amount);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
