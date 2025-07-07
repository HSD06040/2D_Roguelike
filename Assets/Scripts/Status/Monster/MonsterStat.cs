using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : ScriptableObject
{

    public int ID;
    public string monsterName;
    public string monsterDescription;

    [Header("스탯")]
    public float health = 50f;
    public int attackPower = 10;
    public float moveSpeed = 5f;

    public GameObject CoinPrefab;
    public int GetCoinAmount;

    [Header("사운드 설정")] 
    public MonsterSoundType deathSoundType;
}
public enum MonsterSoundType
{
    CreatureDied,
    HumanDied,
    ExplosionMonsterDied, 
    SlimeDied
}
