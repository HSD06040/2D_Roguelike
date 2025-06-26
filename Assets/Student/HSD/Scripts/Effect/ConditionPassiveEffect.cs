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
//    [Tooltip("OnHit(�ǰ� ��), OnAttack(���� ��), OnTime(�����ð� ����), OnKill(óġ ���� ��)")]
//    public PassiveTriggerType passiveTriggerType;

//    public bool IsDiff;
//    [SerializeField] private PassiveFunc passiveFunc;

//[Header("Condition")]
//public bool IsCondition;
//[SerializeField, Tooltip("� ������ �������� �� ���ΰ���?")] 
//private StatType statType;
//[SerializeField, Tooltip("���� ���� �������� �� ���ΰ���?")] 
//private float value;
//[SerializeField, Tooltip("� �������� �����Ͻ� �ǰ���?")] 
//private Condition condition;

//    [Header("Object Spawn")]
//    [Tooltip("������Ʈ�� �����Ͻ� �ǰ���?")]
//    public bool IsObjectSpawn;
//    [SerializeField, Tooltip("� ������Ʈ�� �����Ͻ� �ǰ���?")] 
//    private GameObject spawnObject;
//    [SerializeField, Tooltip("�� ���Ŀ� ������ �ǰ���?")] 
//    private float delay;
//    [SerializeField, Tooltip("�÷��̾�� Offset�� �󸶳� �ǳ���?")] 
//    private Vector3 spawnOffset;

//    [Header("Stat Modifier")]
//    [Tooltip("������ �߰��Ͻ� �� �ΰ���?")] 
//    public bool IsStatModifier;
//    [SerializeField, Tooltip("� ������ �߰��Ͻ� �ǰ���?")] 
//    private StatType modifierStat;
//    [SerializeField, Tooltip("�󸶳� �߰��Ͻ� �ǰ���?")] 
//    private float modifierValue;
//    private bool isAdd;

//    [Header("Chance")]
//    public bool IsChance;
//    [SerializeField, Range(0,100), Tooltip("Ȯ���� �󸶳� �ǳ���?")] 
//    private float chance;

//    [Header("OnTime")]
//    [Tooltip("���� ���� ����� �� �ΰ���?")] 
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
