using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IntStat
{
    private int value;
    private List<Modifier<int>> modifiers = new List<Modifier<int>>();
    private bool isChanged;
    private int lastValue;

    public int Value
    {
        get
        {
            if (isChanged)
            {
                lastValue = value;

                foreach (var modifier in modifiers)
                {
                    lastValue += modifier.value;
                }

                isChanged = false;
            }

            return lastValue;
        }
    }

    public void AddModifier(int value, string source)
    {
        modifiers.Add(new Modifier<int>(value, source));
        isChanged = true;
    }

    public void RemoveModifier(string source)
    {
        modifiers.RemoveAll(modifier => modifier.source == source);
        isChanged = true;
    }

    public void Clear() => modifiers.Clear();
}
