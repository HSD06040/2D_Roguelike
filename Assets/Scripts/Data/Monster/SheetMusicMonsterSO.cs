using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SheetMusicMonsterSO", menuName = "SO/Monsters/SheetMusicMonster")]
public class SheetMusicMonsterSO : MonsterStat
{
    [Header("사정거리")]
    public float chaseRange = 5f; // 추격 범위
    public float meleeAttackRange = 1.5f; // 근접 공격 사거리

    [Header("공격 설정")]
    public float meleeAttackCooldown = 2f; // 공격 쿨타임

    [Header("보상")]
    public GameObject dropItemPrefab;
    public int dropItemCount = 1;
}
