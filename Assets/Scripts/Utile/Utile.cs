using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public static class Utile
{
    private static readonly Dictionary<float, WaitForSeconds> effectDelayDic = new Dictionary<float, WaitForSeconds>();
    private static readonly Dictionary<float, WaitForSecondsRealtime> realTimeDic = new Dictionary<float, WaitForSecondsRealtime>();

    public static WaitForSeconds GetDelay(float dealy)
    {
        if (!effectDelayDic.ContainsKey(dealy))
            effectDelayDic[dealy] = new WaitForSeconds(dealy);

        return effectDelayDic[dealy];
    }
    public static WaitForSecondsRealtime GetRealTimeDelay(float delay)
    {
        if(!realTimeDic.ContainsKey(delay))
            realTimeDic[delay] = new WaitForSecondsRealtime(delay);

        return realTimeDic[delay];
    }

    private static readonly StringBuilder sb = new StringBuilder();

    public static void Apeend<T>(T text)
    {
        sb.Append(text);       
    }

    public static void AppendLine<T>(T text)
    {
        sb.AppendLine(text.ToString());       
    }

    public static string GetText()
    {
        string temp = sb.ToString();
        sb.Clear();
        return temp;
    }
}
