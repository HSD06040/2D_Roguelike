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
    public float attackCooldown = 3f; // 공격 쿨타임 (이 시간동안 이동)
    public GameObject notePrefab;     // 발사할 노트 프리팹 (NoteController를 가진)
    public float noteSpeed = 12f;     // 노트 발사 속도

    
}
