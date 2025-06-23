using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{
    [SerializeField] protected int maxHp;
    [SerializeField] protected int attackPower;
    protected int currentHp;

    protected void Start()
    {
        currentHp = maxHp;
    }
    public virtual void TakeDamage(int damage) //����� �ޱ�
    {
        currentHp -= damage;
        Debug.Log(gameObject.name + "��" + damage + "�� ���ظ� ����. ���� HP:" + currentHp);

        if (currentHp <= 0)
        {
            Debug.Log("���");
        }
    }

    public virtual void Attack(StatusController target) // ����� �ֱ�
    {
        if (target != null)
        {
            target.TakeDamage(attackPower);
        }
    }


}
