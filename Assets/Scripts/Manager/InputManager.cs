using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;

public enum CursorType
{
    Defualt, Attack
}

public class InputManager : Singleton<InputManager>
{
    private InputActionAsset inputActionAsset;
    private InputActionMap playerMap;
    private InputActionMap uIMap;

    private static readonly Dictionary<string, InputBind> playerBind = new Dictionary<string, InputBind>();
    private static readonly Dictionary<string, InputBind> uiBinds = new Dictionary<string, InputBind>();

    private Texture2D defaultCursor;
    private Texture2D attackCursor;

    private void Awake()
    {        
        inputActionAsset = Resources.Load<InputActionAsset>("InputAction");

        defaultCursor = Resources.Load<Texture2D>("Cursor/Default");
        attackCursor = Resources.Load<Texture2D>("Cursor/Attack");

        playerMap = inputActionAsset.FindActionMap("Player");
        uIMap = inputActionAsset.FindActionMap("UI");

        UIBindingSetting();
        PlayerBindSetting();
        ChangeCursor(CursorType.Defualt);
    }

    public void ChangeCursor(CursorType type)
    {
        Vector2 hotspot = Vector2.zero;

        switch (type)
        {
            case CursorType.Defualt:
                hotspot = new Vector2(defaultCursor.width / 2, defaultCursor.height / 2);
                Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
                break;
            case CursorType.Attack:
                hotspot = new Vector2(attackCursor.width / 2, attackCursor.height / 2);
                Cursor.SetCursor(attackCursor, hotspot, CursorMode.Auto);
                break;
        }
    }

    public Vector2 GetMousePosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);

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
