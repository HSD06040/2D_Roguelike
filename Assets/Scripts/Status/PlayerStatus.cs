using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStatus
{
    // 플레이어스텟    
    public IntStat Hp;
    public IntStat Damage;
    public FloatStat Speed;
    public FloatStat AttackSpeed;

    // 플레이어가 현재 가지고 있는 무기 (나중에 추가)
}
