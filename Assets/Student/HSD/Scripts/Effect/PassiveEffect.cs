using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Condition
{
    Equal, GreaterThan, GreaterThanOrEqual, LessThan, LessThanOrEqualOrEqual
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
    private bool isAdding;

    public override void Active(string source)
    {
        if (isAdding) return;

        float currentStat = Manager.Data.PlayerStatus.GetStat(statType);

        switch (condition)
        {
            case Condition.Equal:
                if(currentStat == value)
                {

                }
                break;
        }
    }
}
