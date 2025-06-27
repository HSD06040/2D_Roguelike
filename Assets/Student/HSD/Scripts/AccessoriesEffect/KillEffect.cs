using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KillCountEffect", menuName = "Item/Accessories/Effect/KillCountEffect")]
public class KillEffect : AccessoriesEffect
{
    [Header("Kill Count Effect")]
    [SerializeField, Tooltip("몇 마리 처치할때 마다")] 
    private int[] monsterCounts;

    [SerializeField] private StatEffectStruct[] statEffects;

    private int currentMonsterCount = 0;
    private int totalMonsterCount = 0;

    public override void Active1(Accessories accessories) => TryAddPlayerStat(accessories);

    public override void Active2(Accessories accessories) => TryAddPlayerStat(accessories);

    public override void Active3(Accessories accessories) => TryAddPlayerStat(accessories);

    public override void Active4(Accessories accessories) => TryAddPlayerStat(accessories);

    public override void Revoke(Accessories accessories)
    {
        Manager.Data.PlayerStatus.RemoveStat(statEffects[accessories.UpgradeIdx].statType, accessories.itemName);
        isActive = false;
    }

    public override void Execute(Accessories accessories)
    {
        base.Execute(accessories);
        currentMonsterCount++;
        totalMonsterCount++;
    }

    private void TryAddPlayerStat(Accessories accessories)
    {
        if (isActive)
        {
            if (currentMonsterCount == monsterCounts[accessories.UpgradeIdx])
            {
                currentMonsterCount = 0;
                Manager.Data.PlayerStatus.
                    AddStat(statEffects[accessories.UpgradeIdx].statType, statEffects[accessories.UpgradeIdx].value, accessories.itemName);
            }
        }
        else
        {
            if(totalMonsterCount >= monsterCounts[accessories.UpgradeIdx])
            {
                int count = totalMonsterCount / monsterCounts[accessories.UpgradeIdx];

                for (int i = 0; i < count; i++)
                {
                    Manager.Data.PlayerStatus.
                    AddStat(statEffects[accessories.UpgradeIdx].statType, statEffects[accessories.UpgradeIdx].value, accessories.itemName);
                }
            }

            isActive = true;
        }        
    }
}
