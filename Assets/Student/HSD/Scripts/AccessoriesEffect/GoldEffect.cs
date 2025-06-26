using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldEffect : AccessoriesEffect
{
    [SerializeField] private int[] goldCount;
    [SerializeField] private StatEffectStruct[] stats;

    public override void Active1(Accessories accessories)
    {
        TryAddPlayerStat(accessories);
    }

    public override void Active2(Accessories accessories)
    {
        TryAddPlayerStat(accessories);
    }

    public override void Active3(Accessories accessories)
    {
        TryAddPlayerStat(accessories);
    }

    public override void Active4(Accessories accessories)
    {
        TryAddPlayerStat(accessories);
    }

    public override void Revoke(Accessories accessories)
    {
        if (!isActive) return;

        Manager.Data.PlayerStatus.
                RemoveStat(stats[accessories.UpgradeIdx].statType, $"{accessories.itemName}_{accessories.UpgradeIdx}");

        isActive = false;
    }

    private void TryAddPlayerStat(Accessories accessories)
    {
        if (isActive) return;

        if(Manager.Data.Gold.Value >= goldCount[accessories.UpgradeIdx])
        {
            Manager.Data.PlayerStatus.
                AddStat(stats[accessories.UpgradeIdx].statType, stats[accessories.UpgradeIdx].value, $"{accessories.itemName}_{accessories.UpgradeIdx}");

            isActive = true;
        }        
    }
}
