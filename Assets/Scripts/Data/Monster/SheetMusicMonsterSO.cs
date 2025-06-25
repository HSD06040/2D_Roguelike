using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SheetMusicMonsterSO", menuName = "SO/Monsters/SheetMusicMonster")]
public class SheetMusicMonsterSO : MonsterStat
{
    [Header("�����Ÿ�")]
    public float chaseRange = 5f; // �߰� ����
    public float meleeAttackRange = 1.5f; // ���� ���� ��Ÿ�

    [Header("���� ����")]
    public float meleeAttackCooldown = 2f; // ���� ��Ÿ��

    [Header("����")]
    public GameObject dropItemPrefab;
    public int dropItemCount = 1;
}
