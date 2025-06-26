using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class CoroutineUtile
{
    private static readonly Dictionary<string, Coroutine> effectCoroutineDic = new Dictionary<string, Coroutine>();
    private static readonly Dictionary<float, WaitForSeconds> effectDelayDic = new Dictionary<float, WaitForSeconds>();

    public static Coroutine GetCoroutine(string key)
    {
        if (!effectCoroutineDic.ContainsKey(key))
            return null;
        
        return effectCoroutineDic[key];
    }

    public static void SetCoroutine(string key, Coroutine coroutine)
    {
        effectCoroutineDic[key] = coroutine;
    }

    public static WaitForSeconds GetWait(float dealy)
    {
        if (!effectDelayDic.ContainsKey(dealy))
            effectDelayDic[dealy] = new WaitForSeconds(dealy);

        return effectDelayDic[dealy];
    }
}
