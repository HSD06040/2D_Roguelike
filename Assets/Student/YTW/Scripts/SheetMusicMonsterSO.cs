using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SheetMusicMonsterSO", menuName = "SO/Monsters/SheetMusicMonster")]
public class SheetMusicMonsterSO : ScriptableObject
{
    [Header("Stats")]
    public float health = 50f;
    public float attackPower = 10f;
    public float moveSpeed = 5f;

    [Header("Behavior Stats")]
    public float chaseRange = 5f; // 이 거리 안으로 들어오면 돌진
    public float rangedAttackRange = 15f; // 원거리 공격 사거리

    [Header("Attack Timers")]
    public float rangedAttackCooldown = 3f; // 원거리 공격 쿨타임

    [Header("Ranged Attack")]
    public GameObject notePrefab; // 음표 프리팹
    public float noteSpeed = 10f; // 음표 속도
}
