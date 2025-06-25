using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IntStat
{
    [SerializeField] private int value;
    [SerializeField] private List<Modifier<int>> modifiers = new List<Modifier<int>>();
    private bool isChanged;
    private int lastValue;

    public Action<int> OnChanged;

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
        OnChanged.Invoke(Value);
    }

    public void RemoveModifier(string source)
    {
        modifiers.RemoveAll(modifier => modifier.source == source);
        isChanged = true;
        OnChanged.Invoke(Value);
    }

    public void SetBaseStat(int value)
    {
        this.value = value;
        isChanged = true;
    }
    


    public void Clear() => modifiers.Clear();
}
