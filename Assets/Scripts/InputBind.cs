using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    public void AddStartedEvent(Action<InputAction.CallbackContext> callback)
    {
        action.started += callback;
    } 
    
    public void RemoveStartedEvent(Action<InputAction.CallbackContext> callback)
    {
        action.started -= callback;
    }

    public void AddCanceledEvent(Action<InputAction.CallbackContext> callback)
    {
        action.canceled += callback;
    }

    public void RemoveCanceledEvent(Action<InputAction.CallbackContext> callback)
    {        
        action.canceled -= callback;
    }

    public void ActionEanble() => action.Enable();
    public void ActionDisable() => action.Disable();

    public T ReadValue<T>() where T : struct => action.ReadValue<T>();

    public bool IsPressed() => action.IsPressed();

    public bool WasPressedThisFrame() => action.WasPressedThisFrame();

    public bool WasReleasedThisFrame() => action.WasReleasedThisFrame();
}
