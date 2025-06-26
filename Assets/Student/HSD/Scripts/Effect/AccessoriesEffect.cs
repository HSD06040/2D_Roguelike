using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AccessoriesEffect : ScriptableObject
{
    [Header("Condition")]
    public bool IsCondition;
    [SerializeField, Tooltip("� ������ �������� �� ���ΰ���?")]
    private StatType statType;
    [SerializeField, Tooltip("���� ���� �������� �� ���ΰ���?")]
    private float value;
    [SerializeField, Tooltip("� �������� �����Ͻ� �ǰ���?")]
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
