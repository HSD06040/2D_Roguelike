using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public static class Utile
{
    private static readonly Dictionary<float, WaitForSeconds> effectDelayDic = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetDelay(float dealy)
    {
        if (!effectDelayDic.ContainsKey(dealy))
            effectDelayDic[dealy] = new WaitForSeconds(dealy);

        return effectDelayDic[dealy];
    }

    private static readonly StringBuilder sb = new StringBuilder();

    public static string Apeend<T>(T text)
    {
        sb.Clear();
        sb.Append(text);
        return sb.ToString();
    }

    public static string ApeendLine<T>(T text)
    {
        sb.Clear();
        sb.AppendLine(text.ToString());
        return sb.ToString();
    }
}
