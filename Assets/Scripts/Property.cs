using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Property<T>
{
    private T _value;
    public T Value 
    { 
        get 
        { 
            return _value; 
        } 
        set 
        { 
            if (_value.Equals(value)) 
                return; 

            _value = value;
            
            OnChanged?.Invoke(_value); 
        } 
    }

    private event Action<T> OnChanged;

    public Property()
    {
        _value = default;
    }

    public void AddEvent(Action<T> action) => OnChanged += action;

    public void RemoveEvent(Action<T> action) => OnChanged -= action;

    public void ClearEvent()
    {
        foreach (Action<T> action in OnChanged.GetInvocationList())
        {
            OnChanged -= action;
        }
    }
}
