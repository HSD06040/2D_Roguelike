using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSingleton : MonoBehaviour
{
    private static TestSingleton jht_TestInstance;
    public static TestSingleton JHT_TestInstance { get { return jht_TestInstance; } }

    public JHT_PlayerWeapon playerWeapon;

    private bool isPress;
    public bool IsPress { get { return isPress; } set { isPress = value; OnPress?.Invoke(isPress); } }
    public Action<bool> OnPress;

    private void Awake()
    {
        if(jht_TestInstance == null)
        {
            jht_TestInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
