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

    //Violin위한 이벤트
    private bool isPress;
    public bool IsPress { get { return isPress; } set { isPress = value; OnPress?.Invoke(isPress); } }
    public event Action<bool> OnPress; 
    
    //플레이어 죽었을 때 이벤트
    public bool isDead;
    public bool IsDead { get { return isDead; } set { isDead = value; OnDead?.Invoke(isDead); } }
    public event Action<bool> OnDead;

    //ESC키 입력시 이벤트
    private bool isPause;
    public bool IsPause { get { return isPause; } set { isPause = value; OnPause.Invoke(isPause); } }
    public event Action<bool> OnPause;
    #endregion

    public void TimeStop() => Time.timeScale = 0;
    public void TimeRestart() => Time.timeScale = 1;


    #region Pause

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnEnable()
    {
        OnPause += Pause;
        OnDead += PlayerDead;

    }

    private void OnDisable()
    {
        OnPause -= Pause;
        OnDead -= PlayerDead;
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
            playerController.enabled = false;
            Manager.UI.ShowPopUp<PausePopUp>();
        }
        else
        {
            TimeRestart();
            playerController.enabled = true;
            Manager.UI.ClosePopUp();
        }
    }

    private void PlayerDead(bool _isDead)
    {
        if(_isDead)
        {
            TimeStop();
            playerController.enabled = false;
            Manager.UI.ShowPopUp<PlayerDie>();
        }
    }
    #endregion
}
