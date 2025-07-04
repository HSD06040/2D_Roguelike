using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Events
    public Action OnMonsterKill;
    public Action OnMonsterHit;
    public Action OnPlayerAttack;
    public Action OnRetry;

    //Violin위한 이벤트
    public Property<bool> IsPress = new();
    
    //플레이어 죽었을 때 이벤트
    private bool isDead;
    public bool IsDead { get { return isDead; } set { isDead = value; OnDead?.Invoke(isDead); } }
    public event Action<bool> OnDead;

    //ESC키 입력시 이벤트
    private bool isPause;
    public bool IsPause { get { return isPause; } set { isPause = value; OnPause.Invoke(isPause); } }
    public event Action<bool> OnPause;
    #endregion
    public int currentStage;

    private void Awake()
    {
        currentStage = 1;
    }

    public void TimeStop() => Time.timeScale = 0;
    public void TimeRestart() => Time.timeScale = 1;

    #region Pause

    private void OnEnable()
    {
        OnPause += Pause;
        OnDead += PlayerDead;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isDead)
                IsPause = !IsPause;
        }
    }

    private void Pause(bool _isPause)
    {
        if(_isPause)
        {
            TimeStop();
            Manager.UI.ShowPopUp<PausePopUp>();
        }
        else
        {
            TimeRestart();
            Manager.UI.ClosePopUp();
        }
    }

    private void PlayerDead(bool _isDead)
    {
        if(_isDead)
        {
            TimeStop();
            Manager.UI.ShowPopUp<PlayerDie>();
        }
    }
    #endregion
}
