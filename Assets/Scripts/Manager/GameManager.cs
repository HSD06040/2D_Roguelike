using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Events
    public Action<Transform> OnMonsterKill; // ���Ͱ� �׾�����
    public Action<Transform> OnMonsterHit;  // �÷��̾��� ������ ���Ϳ��� ���� ���� ��
    public Action OnPlayerAttack; // �÷��̾ ���� ���� ��
    #endregion
}
