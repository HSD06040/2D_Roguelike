using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedShotMonsterSO", menuName = "SO/Monsters/RangedShotMonsterSO")]
public class RangedShotMonsterSO : RangedMonsterSO // RangedMonsterSO를 상속받아 기존 속성 재사용
{
    public enum AimType
    {
        Player, 
        Self  
    }

    [Header("확산탄 공격 설정")]
    [Tooltip("한 번에 발사되는 투사체의 총 개수")]
    public int projectileCount = 5;

    [Tooltip("투사체가 퍼지는 총 각도")]
    public float spreadAngle = 45f;

    [Tooltip("한 번의 공격 상태에서 발사하는 횟수")]
    public int shotCount = 3;

    [Header("발사 기준 및 오프셋")]
    public AimType aimType = AimType.Player;

    [Tooltip("연속 발사 시 각 발사 사이의 시간 간격")]
    public float timeBetweenShots = 0.3f;
    public float fireAngleOffset = 0f;
}
