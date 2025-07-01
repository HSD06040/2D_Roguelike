using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedMonsterSO", menuName = "SO/Monsters/RangedMonster")]
public class RangedMonsterSO : MonsterStat
{
    [Header("탐지 및 이동 범위")]
    public float detectionRange = 15f;
    public float minRepositionDistance = 7f;  // 플레이어로부터 유지할 최소 거리
    public float maxRepositionDistance = 12f; // 플레이어로부터 벗어나지 않을 최대 거리

    [Header("공격 설정")]
    public float attackCooldown = 3f; // 공격 쿨타임 
    public GameObject projectilePrefab;     // 발사할 발사체 프리팹
    public float projectileSpeed = 12f;     // 발사체 속도

}
