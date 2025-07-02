using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplosionMonsterSO", menuName = "SO/Monsters/ExplosionMonsterSO")]
public class ExplosionMonsterSO : MonsterStat
{
    [Header("추격 및 공격 설정")]
    public float chaseRange = 10f;  // 플레이어를 감지하고 추격을 시작하는 거리
    public float attackRange = 3f;   // 공격 상태로 전환하는 거리

    [Header("자폭 공격 설정")]
    public float chargeTime = 2f;    // 플레이어를 향해 돌격하는 시간
    public float chargeSpeedMultiplier = 1.5f; // 돌격 시 이동 속도 

    public float explosionDelay = 1f;  // 폭발 범위가 표시된 후 실제 폭발까지의 시간
    public float explosionRadius = 4f; // 실제 데미지가 들어가는 폭발 반경

    [Header("폭발 이펙트 스케일")]
    [Tooltip("타원 이펙트의 X축 크기")]
    public float explosionScaleX = 2.6f;

    [Tooltip("타원 이펙트의 Y축 크기")]
    public float explosionScaleY = 1.4f;

    [Header("이펙트 설정")]
    public GameObject explosionIndicatorPrefab;
    public GameObject explosionEffectPrefab;
}
