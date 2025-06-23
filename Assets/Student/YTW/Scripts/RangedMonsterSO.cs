using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RangedMonsterSO", menuName = "SO/Monsters/RangedMonster")]
public class RangedMonsterSO : ScriptableObject
{
    [Header("�⺻ ����")]
    public float health = 30f;
    public float attackPower = 5f; // ��Ʈ�� ���ݷ�
    public float moveSpeed = 4f;

    [Header("Ž�� �� �̵� ����")]
    public float detectionRange = 15f; // �� �Ÿ� ������ �÷��̾ ������ �ൿ ����
    public float repositionBoxWidth = 10f; // ���� �̵��� �� �簢�� ������ �ʺ�
    public float repositionBoxHeight = 10f; // ���� �̵��� �� �簢�� ������ ����

    [Header("���� ����")]
    public float attackCooldown = 3f; // ���� ��Ÿ�� (�� �ð����� �̵�)
    public GameObject notePrefab;     // �߻��� ��Ʈ ������ (NoteController�� ����)
    public float noteSpeed = 12f;     // ��Ʈ �߻� �ӵ�
}
