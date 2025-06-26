using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedMonsterSO", menuName = "SO/Monsters/RangedMonster")]
public class RangedMonsterSO : MonsterStat
{
    [Header("Ž�� �� �̵� ����")]
    public float detectionRange = 15f;
    public float minRepositionDistance = 7f;  // �÷��̾�κ��� ������ �ּ� �Ÿ�
    public float maxRepositionDistance = 12f; // �÷��̾�κ��� ����� ���� �ִ� �Ÿ�

    [Header("���� ����")]
    public float attackCooldown = 3f; // ���� ��Ÿ�� (�� �ð����� �̵�)
    public GameObject notePrefab;     // �߻��� ��Ʈ ������ (NoteController�� ����)
    public float noteSpeed = 12f;     // ��Ʈ �߻� �ӵ�

    
}
