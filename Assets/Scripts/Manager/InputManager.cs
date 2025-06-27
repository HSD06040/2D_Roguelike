using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private Camera cam;

    private InputActionAsset inputActionAsset;
    private InputActionMap playerMap;
    private InputActionMap uIMap;

    private static readonly Dictionary<string, InputBind> playerBind = new Dictionary<string, InputBind>();
    private static readonly Dictionary<string, InputBind> uiBinds = new Dictionary<string, InputBind>();

    private void Awake()
    {        
        inputActionAsset = Resources.Load<InputActionAsset>("InputAction");

        playerMap = inputActionAsset.FindActionMap("Player");
        uIMap = inputActionAsset.FindActionMap("UI");

        UIBindingSetting();
        PlayerBindSetting();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    public Vector2 GetMousePosition() => cam.ScreenToWorldPoint(Input.mousePosition);

    private void UIBindingSetting()
    {
        foreach (var action in uIMap.actions)
        {
            if(!uiBinds.ContainsKey(action.name))
            {
                uiBinds.Add(action.name, new InputBind(action));
            }
        }
    }

    private void PlayerBindSetting()
    {
        foreach (var action in playerMap.actions)
        {
            if (!playerBind.ContainsKey(action.name))
            {
                playerBind.Add(action.name, new InputBind(action));
            }
        }
    }

    public InputBind GetUIBind(string name)
    {
        uiBinds.TryGetValue(name, out var bind);
        if (bind == null) return null;
        return bind;        
    }

    public InputBind GetPlayerBind(string name)
    {
        playerBind.TryGetValue(name, out var bind);
        if (bind == null) return null;
        return bind;
    }
}
