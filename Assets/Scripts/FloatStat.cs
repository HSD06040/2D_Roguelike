using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Modifier<T>
{
    public T value;
    public string source;

    public Modifier(T value, string source)
    {
        this.value = value;
        this.source = source;
    }
}

[Serializable]
public class FloatStat
{
    private float value;
    private List<Modifier<float>> modifiers = new List<Modifier<float>>();
    private bool isChanged;
    private float lastValue;

    public Action<float> OnChanged;

    public float Value
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

    public void AddModifier(float value, string source)
    {
        modifiers.Add(new Modifier<float>(value, source));
        isChanged = true;
        OnChanged?.Invoke(Value);
    }

    public void RemoveModifier(string source)
    {
        modifiers.RemoveAll(modifier => modifier.source == source);
        isChanged = true;
        OnChanged?.Invoke(Value);
    }

    public void SetBaseStat(float value)
    {
        this.value = value;
        isChanged = true;
    }
   

    public void Clear() => modifiers.Clear();
}
