using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputBind
{
    private InputAction action;

    public InputBind(InputAction _inputAction)
    {
        action = _inputAction;
        action.Enable();
    }

    public void AddStartedEvent(Action callback)
    {
        action.started += _ => callback();
    } 
    
    public void RemoveStartedEvent(Action callback)
    {
        action.started -= _ => callback();
    }

    public void ActionEanble() => action.Enable();
    public void ActionDisable() => action.Disable();

    public T ReadValue<T>() where T : struct => action.ReadValue<T>();

    public bool IsPressed() => action.IsPressed();

    public bool WasPressedThisFrame() => action.WasPressedThisFrame();

    public bool WasReleasedThisFrame() => action.WasReleasedThisFrame();
}
