using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SheetMusicMonsterSO", menuName = "SO/Monsters/SheetMusicMonster")]
public class SheetMusicMonsterSO : MonsterStat
{
    [Header("사정거리")]
    public float chaseRange = 5f; // 이 거리 안으로 들어오면 돌진
    public float meleeAttackRange = 1.5f; // 근접 공격 사거리

    [Header("공격 설정")]
    [Range(0, 360)] public float meleeAttackAngle = 90f; // 부채꼴 공격 각도
    public float meleeAttackCooldown = 2f; // 공격 쿨타임
}
