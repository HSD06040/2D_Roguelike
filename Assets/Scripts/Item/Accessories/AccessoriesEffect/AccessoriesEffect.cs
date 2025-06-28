using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Condition
{
    Equal, Greater, GreaterOrEqual, Less, LessOrEqual
}

public enum PassiveTriggerType
{
    OnEquip ,OnHit, OnAttack, OnInterval, OnKill, OnSetGold, 
}

public abstract class AccessoriesEffect : ScriptableObject
{
    [Header("Condition")]
    [SerializeField] private bool isCondition;
    [SerializeField, Tooltip("어떤 스텟을 조건으로 달 것인가요?")]
    private StatType statType;
    [SerializeField, Tooltip("얼마의 값을 조건으로 달 것인가요?")]
    private float value;
    [SerializeField, Tooltip("어떤 조건으로 설정하실 건가요?")]
    private Condition condition;

    [Header("OnInterval")]
    [SerializeField, Tooltip("몇초 마다")] 
    public float[] intervals;
    [Space]

    public List<PassiveTriggerType> triggerTypes;
    protected bool isActive;
    private bool isSubscribe;
    private Accessories registeredAccessories;

    protected virtual void OnEnable()
    {
        isSubscribe = false;
    }

    public virtual void Execute(Accessories accessories)
    {
        if (isCondition && !IsConditionMet(Manager.Data.PlayerStatus.GetStat(statType)))
            return;

        switch (accessories.UpgradeIdx)
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

    private void ExecuteHandler()
    {
        Execute(registeredAccessories);
    }
    private void ExecuteHandler(int i)
    {
        Execute(registeredAccessories);
    }

    public void RegisterEvents(Accessories accessories)
    {
        registeredAccessories = accessories;

        if (triggerTypes.Count <= 0 || isSubscribe) return;
        
        foreach (var trigger in triggerTypes)
        {            
            switch (trigger)
            {
                case PassiveTriggerType.OnKill: Manager.Game.OnMonsterKill += ExecuteHandler; break;
                case PassiveTriggerType.OnSetGold: Manager.Data.Gold.AddEvent(ExecuteHandler); break;
                case PassiveTriggerType.OnHit: Manager.Game.OnMonsterHit += ExecuteHandler; break;
                case PassiveTriggerType.OnAttack: Manager.Game.OnPlayerAttack += ExecuteHandler; break;
                case PassiveTriggerType.OnEquip: Execute(accessories); break;
                case PassiveTriggerType.OnInterval: Execute(accessories); break;
            }
        }

        isSubscribe = true;
    }

    public void UnregisterEvents()
    {
        if (triggerTypes.Count <= 0 || !isSubscribe) return;

        foreach (var trigger in triggerTypes)
        {
            switch (trigger)
            {
                case PassiveTriggerType.OnKill: Manager.Game.OnMonsterKill -=  ExecuteHandler; break;
                case PassiveTriggerType.OnSetGold: Manager.Data.Gold.RemoveEvent(ExecuteHandler); break;
                case PassiveTriggerType.OnHit: Manager.Game.OnMonsterHit -= ExecuteHandler; break;
                case PassiveTriggerType.OnAttack: Manager.Game.OnPlayerAttack -= ExecuteHandler; break;
            }
        }
        isSubscribe = false;
    }
}
