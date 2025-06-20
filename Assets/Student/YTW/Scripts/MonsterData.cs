using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Monster/Monster Data")]
public class MonsterData : ScriptableObject
{
    [Header("기본 정보")]
    public string _monsterName = "몬스터"; // 몬스터 이름 (UI나 디버깅용)

    [Header("핵심 스탯")]
    public float _health = 100f;         // 최대 체력
    public float _atk = 10f;     // 공격력
    public float _speed = 3.5f;      // 이동 속도

    [Header("공격 관련")]
    public GameObject _projectilePrefab; // 발사할 투사체 프리팹
    public float _attackRange = 10f;     // 공격을 시작하는 사정거리
    public float _attackCooldown = 2f;   // 공격 후 다음 공격까지의 대기 시간
}
