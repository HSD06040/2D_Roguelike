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

//[CreateAssetMenu(menuName = "Effect/Condition")]
//public class ConditionPassiveEffect : ItemEffect
//{
//    [Tooltip("OnHit(피격 시), OnAttack(공격 시), OnTime(일정시간 마다), OnKill(처치 했을 때)")]
//    public PassiveTriggerType passiveTriggerType;

//    public bool IsDiff;
//    [SerializeField] private PassiveFunc passiveFunc;

//[Header("Condition")]
//public bool IsCondition;
//[SerializeField, Tooltip("어떤 스텟을 조건으로 달 것인가요?")] 
//private StatType statType;
//[SerializeField, Tooltip("얼마의 값을 조건으로 달 것인가요?")] 
//private float value;
//[SerializeField, Tooltip("어떤 조건으로 설정하실 건가요?")] 
//private Condition condition;

//    [Header("Object Spawn")]
//    [Tooltip("오브젝트를 스폰하실 건가요?")]
//    public bool IsObjectSpawn;
//    [SerializeField, Tooltip("어떤 오브젝트를 스폰하실 건가요?")] 
//    private GameObject spawnObject;
//    [SerializeField, Tooltip("몇 초후에 생성될 건가요?")] 
//    private float delay;
//    [SerializeField, Tooltip("플레이어에서 Offset은 얼마나 되나요?")] 
//    private Vector3 spawnOffset;

//    [Header("Stat Modifier")]
//    [Tooltip("스텟을 추가하실 것 인가요?")] 
//    public bool IsStatModifier;
//    [SerializeField, Tooltip("어떤 스텟을 추가하실 건가요?")] 
//    private StatType modifierStat;
//    [SerializeField, Tooltip("얼마나 추가하실 건가요?")] 
//    private float modifierValue;
//    private bool isAdd;

//    [Header("Chance")]
//    public bool IsChance;
//    [SerializeField, Range(0,100), Tooltip("확률은 얼마나 되나요?")] 
//    private float chance;

//    [Header("OnTime")]
//    [Tooltip("몇초 마다 실행될 것 인가요?")] 
//    public float duration;

//    public override void Execute(string _source, int _upgrade)
//    {
//        if(IsCondition)
//        {
//            if(IsConditionMet(Manager.Data.PlayerStatus.GetStat(statType)))
//            {
//                Active(_source, _upgrade);
//            }
//            else if(isAdd)
//            {
//                Revoke(_source, _upgrade);
//            }
//        }
//        else
//        {
//            Active(_source, _upgrade);
//        }
//    }

//    private void Active(string _source, int _upgrade)
//    {
//        if (IsObjectSpawn)
//        {
//            if (IsChance)
//            {
//                if (Random.Range(0, 100) < chance)
//                {
//                    Instantiate(spawnObject);
//                }
//            }
//            else
//            {
//                Instantiate(spawnObject);
//            }
//        }

//        if (IsStatModifier)
//        {
//            Manager.Data.PlayerStatus.AddStat(modifierStat, modifierValue, $"{_source}_{_upgrade}");
//            isAdd = true;
//        }
//    }

//private bool IsConditionMet(float currentStat)
//{
//    return condition switch
//    {
//        Condition.Equal => Mathf.Approximately(currentStat, value),
//        Condition.Greater => currentStat > value,
//        Condition.Less => currentStat < value,
//        Condition.GreaterOrEqual => currentStat >= value,
//        Condition.LessOrEqual => currentStat <= value,
//        _ => false
//    };
//}

//    public override void Revoke(string _source, int _upgrade)
//    {
//        if (!isAdd) return;

//        Manager.Data.PlayerStatus.RemoveStat(modifierStat, $"{_source}_{_upgrade}");
//        isAdd = false;
//    }
//}
