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
    private const int maxUpgrade = 4;
    private int upgradeIdx;

    public void Upgrade()
    {
        Effect.DeActive(itemName, upgradeIdx);
        upgradeIdx++;
        Effect.Active(itemName, upgradeIdx);
    }
}
