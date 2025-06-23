using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T CreateManager()
    {
        if (instance == null)
        {
            GameObject go = new GameObject(typeof(T).Name);
            instance = go.AddComponent<T>();
            DontDestroyOnLoad(go);
        }
        return instance;
    }

    public static void ReleaseManager()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
            instance = null;
        }
    }

    public static T GetInstance()
    {
        return instance;
    }
}
