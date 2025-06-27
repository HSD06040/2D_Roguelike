using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class CoroutineUtile
{
    private static readonly Dictionary<float, WaitForSeconds> effectDelayDic = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetDelay(float dealy)
    {
        if (!effectDelayDic.ContainsKey(dealy))
            effectDelayDic[dealy] = new WaitForSeconds(dealy);

        return effectDelayDic[dealy];
    }
}
