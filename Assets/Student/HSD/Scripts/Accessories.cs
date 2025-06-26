using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessories : Item
{
    public ConditionPassiveEffect Effect;

    [Header("Stat")]
    public int Damage;
    public float AttackSpeed;
    public float Speed;

    [Header("Upgrade")]
    private const int maxUpgrade = 3;
    public int UpgradeIdx;

    public void Upgrade()
    {
        Effect.Revoke(itemName, UpgradeIdx);
        UpgradeIdx++;
        Effect.Execute(itemName, UpgradeIdx);
    }
}
