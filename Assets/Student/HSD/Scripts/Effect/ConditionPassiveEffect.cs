using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Condition
{
    Equal, Greater, GreaterOrEqual, Less, LessOrEqual
}

public enum PassiveTriggerType
{
    OnHit, OnAttack, OnTime, OnKill
}

[CreateAssetMenu(menuName = "Effect/Condition")]
public class ConditionPassiveEffect : ItemEffect
{
    [Tooltip("OnHit(�ǰ� ��), OnAttack(���� ��), OnTime(�����ð� ����), OnKill(óġ ���� ��)")]
    public PassiveTriggerType passiveTriggerType;

    [Header("Condition")]
    public bool IsCondition;
    [SerializeField] private StatType statType;
    [SerializeField] private float value;
    [SerializeField] private Condition condition;

    [Header("Object Spawn")]
    public bool IsObjectSpawn;
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private float delay;
    [SerializeField] private Vector3 spawnOffset;

    [Header("Stat Modifier")]
    public bool IsStatModifier;
    [SerializeField] private StatType modifierStat;
    [SerializeField] private float modifierValue;
    private bool isAdd;

    [Header("Chance")]
    public bool IsChance;
    [SerializeField, ] private float chance;

    [Header("OnTime")]
    [Tooltip("���� ����")] 
    public float duration;

    public override void Execute(string _source, int _upgrade)
    {
        if(IsCondition)
        {
            if(IsConditionMet(Manager.Data.PlayerStatus.GetStat(statType)))
            {
                Active(_source, _upgrade);
            }
            else if(isAdd)
            {
                Revoke(_source, _upgrade);
            }
        }
        else
        {
            Active(_source, _upgrade);
        }
    }

    private void Active(string _source, int _upgrade)
    {
        if (IsObjectSpawn)
        {
            if (IsChance)
            {
                if (Random.Range(0, 100) > chance)
                {
                    Instantiate(spawnObject);
                }
            }
            else
            {
                Instantiate(spawnObject);
            }
        }

        if (IsStatModifier)
        {
            Manager.Data.PlayerStatus.AddStat(modifierStat, modifierValue, $"{_source}_{_upgrade}");
            isAdd = true;
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

    public override void Revoke(string _source, int _upgrade)
    {
        if (!isAdd) return;

        Manager.Data.PlayerStatus.RemoveStat(statType, $"{_source}_{_upgrade}");
        isAdd = false;
    }
}
