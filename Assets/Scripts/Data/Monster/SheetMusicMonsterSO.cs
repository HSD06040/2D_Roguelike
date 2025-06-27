using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SheetMusicMonsterSO", menuName = "SO/Monsters/SheetMusicMonster")]
public class SheetMusicMonsterSO : MonsterStat
{
    [Header("탐지 범위")]
    public float chaseRange = 5f; // 추격 범위

    [Header("공격 설정")]
    public float meleeAttackRange = 1.5f; // 부채꼴의 반지름(크기)
    public float meleeAttackAngle = 60f;  //  부채꼴의 총 각도
    public float meleeAttackCooldown = 2f; // 공격 쿨타임

    [Header("보상")]
    public GameObject dropItemPrefab;
    public int dropItemCount = 1;
}
