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

    private bool isPress; //�÷��̾ ���콺�� ������ ��
    public bool IsPress { get { return isPress; } set { isPress = value; OnPress?.Invoke(isPress); } }
    public event Action<bool> OnPress;
    #endregion


}
