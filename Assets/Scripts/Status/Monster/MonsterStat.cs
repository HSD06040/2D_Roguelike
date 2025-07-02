using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : ScriptableObject
{
    public int ID;
    public string monsterName;
    public string monsterDescription;

    [Header("Ω∫≈»")]
    public float health = 50f;
    public int attackPower = 10;
    public float moveSpeed = 5f;

    public GameObject CoinPrefab;
    public int GetCoinAmount;
}
