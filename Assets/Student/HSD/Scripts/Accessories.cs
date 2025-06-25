using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessories : Item
{
    public ItemEffect Effect;

    [Header("Stat")]
    public int Damage;
    public float AttackSpeed;
    public float Speed;

    [Header("Upgrade")]
    private const int maxUpgrade = 3;
    private int upgradeIdx;

    public void Upgrade()
    {
        Effect.Revoke(itemName, upgradeIdx);
        upgradeIdx++;
        Effect.Execute(itemName, upgradeIdx);
    }
}
