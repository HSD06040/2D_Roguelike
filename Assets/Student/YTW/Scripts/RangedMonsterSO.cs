using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedMonsterSO", menuName = "SO/Monsters/RangedMonster")]
public class RangedMonsterSO : ScriptableObject
{
    [Header("기본 스탯")]
    public float health = 30f;
    public float attackPower = 5f; // 노트의 공격력
    public float moveSpeed = 4f;

    [Header("탐지 및 이동 범위")]
    public float detectionRange = 15f; // 이 거리 안으로 플레이어가 들어오면 행동 시작
    public float repositionBoxWidth = 10f; // 랜덤 이동을 할 사각형 범위의 너비
    public float repositionBoxHeight = 10f; // 랜덤 이동을 할 사각형 범위의 높이

    [Header("공격 설정")]
    public float attackCooldown = 3f; // 공격 쿨타임 (이 시간동안 이동)
    public GameObject notePrefab;     // 발사할 노트 프리팹 (NoteController를 가진)
    public float noteSpeed = 12f;     // 노트 발사 속도
}
