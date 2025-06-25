using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Condition
{
    Equal, Greater, GreaterOrEqual, Less, LessOrEqual
}

public class PassiveEffect : ItemEffect
{
    [Header("Condition")]
    [SerializeField] private bool isCondition;
    [SerializeField] private StatType statType;
    [SerializeField] private float value;
    [SerializeField] private Condition condition;

    [Header("Modifier")]
    [SerializeField] private StatType modifierStat;
    [SerializeField] private float modifierValue;
    public bool isAdding;

    public override void Active(string _source, int _upgrade)
    {
        if (isAdding) return;

        Manager.Data.PlayerStatus.AddStat(modifierStat, value, _source);

        isAdding = true;
    }

    public override void Check(string _source, int _upgrade)
    {
        float currentStat = Manager.Data.PlayerStatus.GetStat(statType);

        if (IsConditionMet(currentStat))
        {
            Active($"{_source}_{_upgrade}", _upgrade);
        }
        else if (isAdding)
        {
            DeActive($"{_source}_{_upgrade}", _upgrade);
        }
    }

    private bool IsConditionMet(float currentStat)
    {
        return condition switch
        {
            Condition.Equal => Mathf.Approximately(currentStat, value),
            Condition.Greater => currentStat > value,
            Condition.Less => currentStat < value,
            Condition.GreaterOrEqual => currentStat >= value,
            Condition.LessOrEqual => currentStat <= value,
            _ => false
        };
    }

    public override void DeActive(string _source, int _upgrade)
    {
        if (!isAdding) return;

        isAdding = false;
    }
}
