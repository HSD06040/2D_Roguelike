using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour, IDamagable
{
    [SerializeField] protected int maxHp;
    [SerializeField] protected int attackPower;
    [SerializeField] protected int currentHp;

    protected void Start()
    {
        currentHp = maxHp;
    }

    public virtual void TakeDamage(int damage) //대미지 받기
    {
        currentHp -= damage;
        Debug.Log(gameObject.name + "이" + damage + "의 피해를 받음. 현재 HP:" + currentHp);

        if (currentHp <= 0)
        {
            Debug.Log("사망");
        }
    }

    public virtual void Attack(StatusController target) // 대미지 주기
    {
        if (target != null)
        {
            target.TakeDamage(attackPower);
        }
    }


}
