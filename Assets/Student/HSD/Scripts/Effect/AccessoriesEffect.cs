using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AccessoriesEffect : ScriptableObject
{
    [Header("Condition")]
    public bool IsCondition;
    [SerializeField, Tooltip("어떤 스텟을 조건으로 달 것인가요?")]
    private StatType statType;
    [SerializeField, Tooltip("얼마의 값을 조건으로 달 것인가요?")]
    private float value;
    [SerializeField, Tooltip("어떤 조건으로 설정하실 건가요?")]
    private Condition condition;

    [SerializeField] private List<PassiveTriggerType> triggerTyps;
    protected bool isActive;

    public virtual void Execute(Accessories accessories)
    {
        if(IsCondition)        
            if (IsConditionMet(Manager.Data.PlayerStatus.GetStat(statType))) return;        

        switch(accessories.UpgradeIdx)
        {
            case 0: Active1(accessories); break;
            case 1: Active2(accessories); break;
            case 2: Active3(accessories); break;
            case 3: Active4(accessories); break;
        }
    }

    public abstract void Active1(Accessories accessories);
    public abstract void Active2(Accessories accessories);
    public abstract void Active3(Accessories accessories);
    public abstract void Active4(Accessories accessories);
    public abstract void Revoke(Accessories accessories);
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

    public void RegisterEvents()
    {
        if (triggerTyps.Count <= 0) return;

        foreach (var trigger in triggerTyps)
        {
            switch(trigger)
            {
                case PassiveTriggerType.OnKill: Manager.Game.OnMonsterKill += EventTrigger; break;
            }
        }
    }

    public void UnregisterEvents()
    {
        foreach (var trigger in triggerTyps)
        {
            switch (trigger)
            {
                case PassiveTriggerType.OnKill: Manager.Game.OnMonsterKill -= EventTrigger; break;
            }
        }
    }

    protected virtual void EventTrigger() { }
}
