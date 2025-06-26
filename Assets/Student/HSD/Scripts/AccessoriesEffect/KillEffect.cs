using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEffect : AccessoriesEffect
{
    [SerializeField, Tooltip("몇 마리 처치할때 마다")] 
    private int[] monsterCounts;

    [SerializeField] private StatEffectStruct[] statEffects;

    private int currentMonsterCount = 0;

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
        Manager.Data.PlayerStatus.RemoveStat(statEffects[accessories.UpgradeIdx].statType, accessories.itemName);
    }

    protected override void EventTrigger()
    {
        currentMonsterCount++;
    }
    private void TryAddPlayerStat(Accessories accessories)
    {
        if (currentMonsterCount == monsterCounts[accessories.UpgradeIdx])
        {
            currentMonsterCount = 0;
            Manager.Data.PlayerStatus.
                AddStat(statEffects[accessories.UpgradeIdx].statType, statEffects[accessories.UpgradeIdx].value, accessories.itemName);
        }
    }
}
