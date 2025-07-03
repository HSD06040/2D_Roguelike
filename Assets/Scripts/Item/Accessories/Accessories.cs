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
        if (Damage.Length > UpgradeIdx && Damage[UpgradeIdx] != 0)
            Manager.Data.PlayerStatus.AddStat(StatType.Damage, Damage[UpgradeIdx], itemName);

        if (DamageMultiply.Length > UpgradeIdx && DamageMultiply[UpgradeIdx] != 0)
            Manager.Data.PlayerStatus.AddStat(StatType.DamageMultiply, DamageMultiply[UpgradeIdx], itemName);

        if (Speed.Length > UpgradeIdx && Speed[UpgradeIdx] != 0)
            Manager.Data.PlayerStatus.AddStat(StatType.Speed, Speed[UpgradeIdx], itemName);

        if (SpeedMultiply.Length > UpgradeIdx && SpeedMultiply[UpgradeIdx] != 0)
            Manager.Data.PlayerStatus.AddStat(StatType.SpeedMultiply, SpeedMultiply[UpgradeIdx], itemName);

        if (AttackSpeed.Length > UpgradeIdx && AttackSpeed[UpgradeIdx] != 0)
            Manager.Data.PlayerStatus.AddStat(StatType.AttackSpeed, AttackSpeed[UpgradeIdx], itemName);

        if (Evasion.Length > UpgradeIdx && Evasion[UpgradeIdx] != 0)
            Manager.Data.PlayerStatus.AddStat(StatType.Evasion, Evasion[UpgradeIdx], itemName);
    }

    public void RemoveStat()
    {
        if (Damage.Length > 0 && Damage[UpgradeIdx] != 0)
            Manager.Data.PlayerStatus.RemoveStat(StatType.Damage, itemName);

        if (DamageMultiply.Length > 0 && DamageMultiply[UpgradeIdx] != 0)
            Manager.Data.PlayerStatus.RemoveStat(StatType.DamageMultiply, itemName);

        if (Speed.Length > 0 && Speed[UpgradeIdx] != 0)
            Manager.Data.PlayerStatus.RemoveStat(StatType.Speed, itemName);

        if (SpeedMultiply.Length > 0 && SpeedMultiply[UpgradeIdx] != 0)
            Manager.Data.PlayerStatus.RemoveStat(StatType.SpeedMultiply, itemName);

        if (AttackSpeed.Length > 0 && AttackSpeed[UpgradeIdx] != 0)
            Manager.Data.PlayerStatus.RemoveStat(StatType.AttackSpeed, itemName);

        if (Evasion.Length > 0 && Evasion[UpgradeIdx] != 0)
            Manager.Data.PlayerStatus.RemoveStat(StatType.Evasion, itemName);
    }

    public string GetStatText()
    {
        if (Damage.Length > UpgradeIdx && Damage[UpgradeIdx] != 0)
            Utile.AppendLine($"공격력 : {Damage[UpgradeIdx]}");

        if (DamageMultiply.Length > UpgradeIdx && DamageMultiply[UpgradeIdx] != 0)
            Utile.AppendLine($"공격력% : {DamageMultiply[UpgradeIdx]}");

        if (Speed.Length > UpgradeIdx && Speed[UpgradeIdx] != 0)
            Utile.AppendLine($"이동속도 : {Speed[UpgradeIdx]}");

        if (SpeedMultiply.Length > UpgradeIdx && SpeedMultiply[UpgradeIdx] != 0)
            Utile.AppendLine($"이동속도% : {SpeedMultiply[UpgradeIdx]}");

        if (AttackSpeed.Length > UpgradeIdx && AttackSpeed[UpgradeIdx] != 0)
            Utile.AppendLine($"공격속도 : {AttackSpeed[UpgradeIdx]}");

        if (Evasion.Length > UpgradeIdx && Evasion[UpgradeIdx] != 0)
            Utile.AppendLine($"회피율 : {Evasion[UpgradeIdx]}");

        return Utile.GetText();
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
