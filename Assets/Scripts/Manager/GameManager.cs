using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Events
    public Action OnMonsterKill; // 몬스터가 죽었을때
    public Action OnMonsterHit;  // 플레이어의 공격이 몬스터에게 적중 했을 때
    public Action OnPlayerAttack; // 플레이어가 공격 했을 때
    #endregion
}
