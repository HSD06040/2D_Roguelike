using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StatEffectStruct
{
    [Tooltip("어떤 스텟을 늘릴 것 인지")] 
    public StatType statType;

    [Tooltip("얼만큼 늘릴 것 인지")] 
    public float value;
}

[CreateAssetMenu(fileName = "StatEffect", menuName = "Item/Accessories/Effect/StatEffect")]
public class StatEffect : AccessoriesEffect
{
    [Header("Stat Effect")]
    [SerializeField, Tooltip("!! 4개 까지 설정")]
    private StatEffectStruct[] stats;

    public override void Active1(Accessories accessories) => AddStat(accessories);

    public override void Active2(Accessories accessories) => AddStat(accessories);

    public override void Active3(Accessories accessories) => AddStat(accessories);

    public override void Active4(Accessories accessories) => AddStat(accessories);

    public override void Revoke(Accessories accessories)
    {
        if (!isActive) return;

        Manager.Data.PlayerStatus.RemoveStat(stats[accessories.UpgradeIdx].statType, $"{accessories.itemName}_{accessories.UpgradeIdx}");
        isActive = false;
    }

    private void AddStat(Accessories accessories)
    {
        if (isActive) return;

        Manager.Data.PlayerStatus.AddStat
                    (stats[accessories.UpgradeIdx].statType, stats[accessories.UpgradeIdx].value, $"{accessories.itemName}_{accessories.UpgradeIdx}");

        isActive = true;
    }
}
