using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Manager
{
    public static GameManager Game => GameManager.GetInstance();
    public static PoolManager Pool => PoolManager.GetInstance();
    public static ResourcesManager Resources => ResourcesManager.GetInstance();
    public static AudioManager Audio => AudioManager.GetInstance();
    public static UI_Manager UI => UI_Manager.GetInstance();
    public static DataManager Data => DataManager.GetInstance();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        GameManager.CreateManager();
        PoolManager.CreateManager();
        ResourcesManager.CreateManager();
        AudioManager.CreateManager();
        UI_Manager.CreateManager();
        DataManager.CreateManager();
    }
}
