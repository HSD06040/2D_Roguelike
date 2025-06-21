using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SheetMusicMonsterSO", menuName = "SO/Monsters/SheetMusicMonster")]
public class SheetMusicMonsterSO : ScriptableObject
{
    [Header("스탯")]
    public float health = 50f;
    public float attackPower = 10f;
    public float moveSpeed = 5f;

    [Header("사정거리")]
    public float chaseRange = 5f; // 이 거리 안으로 들어오면 돌진
    public float meleeAttackRange = 1.5f; // 근접 공격 사거리

    [Header("공격 쿨타임")]
    public float meleeAttackCooldown = 2f; // 공격 쿨타임

    [Header("원거리 공격")]
    public GameObject notePrefab; // 음표 프리팹
    public float noteSpeed = 10f; // 음표 속도
}
