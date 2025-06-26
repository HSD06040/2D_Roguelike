using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessories : Item
{
    public AccessoriesEffect Effect;

    [Header("Stat")]
    public int Damage;
    public float AttackSpeed;
    public float Speed;

    [Header("Upgrade")]
    private const int maxUpgrade = 4;
    public int UpgradeIdx;

    public void Upgrade()
    {        
        Effect.Revoke(this);
        UpgradeIdx++;
        Effect.Execute(this);
    }
}
