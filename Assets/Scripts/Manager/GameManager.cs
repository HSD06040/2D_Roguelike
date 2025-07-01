using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Events
    public Action OnMonsterKill;
    public Action OnMonsterHit;
    public Action OnPlayerAttack;

    private bool isPress;
    public bool IsPress { get { return isPress; } set { isPress = value; OnPress?.Invoke(isPress); } }
    public event Action<bool> OnPress;
    #endregion

    public void TimeStop() => Time.timeScale = 0;
    public void TimeRestart() => Time.timeScale = 1;
}
