using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplosionMonsterSO", menuName = "SO/Monsters/ExplosionMonsterSO")]
public class ExplosionMonsterSO : MonsterStat
{
    [Header("�߰� �� ���� ����")]
    public float chaseRange = 10f;  // �÷��̾ �����ϰ� �߰��� �����ϴ� �Ÿ�
    public float attackRange = 3f;   // ���� ���·� ��ȯ�ϴ� �Ÿ�

    [Header("���� ���� ����")]
    public float chargeTime = 2f;    // �÷��̾ ���� �����ϴ� �ð�
    public float chargeSpeedMultiplier = 1.5f; // ���� �� �̵� �ӵ� 

    public float explosionDelay = 1f;  // ���� ������ ǥ�õ� �� ���� ���߱����� �ð�
    public float explosionRadius = 4f; // ���� �������� ���� ���� �ݰ�

    [Header("���� ����Ʈ ������")]
    [Tooltip("Ÿ�� ����Ʈ�� X�� ũ��")]
    public float explosionScaleX = 2.6f;

    [Tooltip("Ÿ�� ����Ʈ�� Y�� ũ��")]
    public float explosionScaleY = 1.4f;

    [Header("����Ʈ ����")]
    public GameObject explosionIndicatorPrefab;
    public GameObject explosionEffectPrefab;
}
