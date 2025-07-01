using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Accessories", menuName = "Item/AccessoriesItem")]
public class Accessories : Item
{
    public AccessoriesEffect Effect;

    [Header("Stat")]
    public float[] Damage;
    public float[] DamageMultiply;
    public float[] Speed;
    public float[] SpeedMultiply;
    public float[] AttackSpeed;
    public float[] Evasion;
    public float[] AttackSize;

    [Header("Upgrade")]
    private const int maxUpgrade = 3;
    private bool isAttack;
    public int UpgradeIdx;

    private void OnEnable()
    {
        UpgradeIdx = 0;
        isAttack = false;
    }

    public void AddStat()
    {
        
    }

    public void RemoveStat()
    {

    }

    public void GetStatText()
    {

    }

    public void Upgrade()
    {
        if (UpgradeIdx >= maxUpgrade) return;

        Effect.Revoke(this);
        UpgradeIdx++;

        foreach (var type in Effect.triggerTypes)
        {
            if (type == PassiveTriggerType.OnAttack)
                isAttack = true;
        }

        if(!isAttack)
            Effect.Execute(this);
    }
}
