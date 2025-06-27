using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SheetMusicMonsterSO", menuName = "SO/Monsters/SheetMusicMonster")]
public class SheetMusicMonsterSO : MonsterStat
{
    [Header("Ž�� ����")]
    public float chaseRange = 5f; // �߰� ����

    [Header("���� ����")]
    public float meleeAttackRange = 1.5f; // ��ä���� ������(ũ��)
    public float meleeAttackAngle = 60f;  //  ��ä���� �� ����
    public float meleeAttackCooldown = 2f; // ���� ��Ÿ��

    [Header("����")]
    public GameObject dropItemPrefab;
    public int dropItemCount = 1;
}
