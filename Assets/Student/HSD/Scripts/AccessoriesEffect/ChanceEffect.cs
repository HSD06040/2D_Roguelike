using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChanceEffect", menuName = "Item/Accessories/Effect/ChanceEffect")]
public class ChanceEffect : AccessoriesEffect
{
    [Header("Chance")]
    [SerializeField] private bool isMultiply;    
    [SerializeField, Range(0, 100)] private float[] chances;
    [SerializeField] private int[] amounts;

    public override void Active1(Accessories accessories)
    {
        AddGoldStat(accessories);
    }

    public override void Active2(Accessories accessories)
    {
        AddGoldStat(accessories);
    }

    public override void Active3(Accessories accessories)
    {
        AddGoldStat(accessories);
    }

    public override void Active4(Accessories accessories)
    {
        AddGoldStat(accessories);
    }

    public override void Revoke(Accessories accessories)
    {
        if (isMultiply)
        {
            Manager.Data.GoldStat.KillGoldAmount.RemoveModifier($"{accessories.itemName}_{accessories.UpgradeIdx}");
            Manager.Data.GoldStat.KillGoldChance.RemoveModifier($"{accessories.itemName}_{accessories.UpgradeIdx}");
        }
        else
        {
            Manager.Data.GoldStat.HitGoldAmount.RemoveModifier($"{accessories.itemName}_{accessories.UpgradeIdx}");
            Manager.Data.GoldStat.HitGoldChance.RemoveModifier($"{accessories.itemName}_{accessories.UpgradeIdx}");
        }
    }

    private void AddGoldStat(Accessories accessories)
    {
        if(isMultiply)
        {
            Manager.Data.GoldStat.KillGoldAmount.AddModifier(amounts[accessories.UpgradeIdx], $"{accessories.itemName}_{accessories.UpgradeIdx}");
            Manager.Data.GoldStat.KillGoldChance.AddModifier(chances[accessories.UpgradeIdx], $"{accessories.itemName}_{accessories.UpgradeIdx}");
        }
        else
        {
            Manager.Data.GoldStat.HitGoldAmount.AddModifier(amounts[accessories.UpgradeIdx], $"{accessories.itemName}_{accessories.UpgradeIdx}");
            Manager.Data.GoldStat.HitGoldChance.AddModifier(chances[accessories.UpgradeIdx], $"{accessories.itemName}_{accessories.UpgradeIdx}");
        }
    }
}
