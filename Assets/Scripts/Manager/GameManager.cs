using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Events
    public Action OnMonsterKill; // ���Ͱ� �׾�����
    public Action OnMonsterHit;  // �÷��̾��� ������ ���Ϳ��� ���� ���� ��
    public Action OnPlayerAttack; // �÷��̾ ���� ���� ��
    #endregion
}
