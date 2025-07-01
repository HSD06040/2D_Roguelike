using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedShotMonsterSO", menuName = "SO/Monsters/RangedShotMonsterSO")]
public class RangedShotMonsterSO : RangedMonsterSO 
{
    public enum AimType
    {
        Player,
        LockOnPlayer,
        Self  
    }

    [Header("Ȯ��ź ���� ����")]
    [Tooltip("�� ���� �߻�Ǵ� ����ü�� �� ����")]
    public int projectileCount = 5;

    [Tooltip("����ü�� ������ �� ����")]
    public float spreadAngle = 45f;

    [Tooltip("�� ���� ���� ���¿��� �߻��ϴ� Ƚ��")]
    public int shotCount = 3;

    [Header("�߻� ���� �� ������")]
    public AimType aimType = AimType.Player;

    [Tooltip("���� �߻� �� �� �߻� ������ �ð� ����")]
    public float timeBetweenShots = 0.3f;
    public float fireAngleOffset = 0f;

    [Header("������ ź�� ����")]
    public float spiralRotationSpeed = 0f;

}
