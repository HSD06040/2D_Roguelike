using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldStat
{
    public FloatStat HitGoldChance = new FloatStat();
    public IntStat HitGoldAmount = new IntStat();

    public FloatStat KillGoldChance = new FloatStat();
    public IntStat KillGoldAmount = new IntStat();

    public void InitGoldStat()
    {
        HitGoldChance = new FloatStat();
        HitGoldAmount = new IntStat();
        KillGoldAmount = new IntStat();
        KillGoldChance = new FloatStat();
    }
}
