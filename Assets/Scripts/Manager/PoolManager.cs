using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, IObjectPool<GameObject>> poolDic;
    private Dictionary<string, Transform> parentDic;
    private Dictionary<string, float> lastUseTimeDic;

    [Header("PopupText")]
    private Transform popupCanvas;
    private Transform popupParent;
    private ObjectPool<GameObject> popUpPool;

    private Transform parent;

    private Coroutine poolCleanupRoutine;

    private const float poolCleanupTime = 60;
    private const float poolCleanupDelay = 30;

    public void Start()
    {
        ResetPool();
        poolCleanupRoutine = StartCoroutine(PoolCleanupRoutine());

        popupParent = new GameObject("PopupTextParent").transform;
        popupParent.parent = Manager.UI.WorldCanvas.transform;
    }

    public void ResetPool()
    {
        poolDic = new();
        parentDic = new();
        lastUseTimeDic = new();

        parent = new GameObject("Pools").transform;
    }

    private ObjectPool<GameObject> CreatePopUpTextPool(GameObject popUp)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            () =>
            {
                var obj = Instantiate(popUp, popupParent);
                obj.name = popUp.name;
                return obj;
            },
            obj =>
            {
                obj.SetActive(true);
            },
            obj =>
            {
                obj.SetActive(false);
            },
            obj =>
            {
                Destroy(obj);
            },
            maxSize: 15
        );

        for (int i = 0; i < 15; i++)
        {
            pool.Get().SetActive(false);
        }

        return pool;
    }

    IEnumerator PoolCleanupRoutine()
    {
        while (true)
        {
            yield return Utile.GetDelay(poolCleanupDelay);

            float now = Time.time;
            List<string> removePoolKeys = new List<string>();

            foreach (var kvp in poolDic)
            {
                string key = kvp.Key;

                if (lastUseTimeDic.TryGetValue(key, out float lastTime))
                {
                    if (now - lastTime > poolCleanupTime)
                    {
                        removePoolKeys.Add(key);
                    }
                }
            }

            foreach (var value in removePoolKeys)
            {
                poolDic.Remove(value);

                if (parentDic[value].gameObject != null)
                    Destroy(parentDic[value].gameObject);

                parentDic.Remove(value);
                lastUseTimeDic.Remove(value);
            }
        }
    }

    public void StopPoolCleanupRoutine()
    {
        StopCoroutine(poolCleanupRoutine);
        poolCleanupRoutine = null;
    }

    private IObjectPool<GameObject> GetOrCreatePool(string name, GameObject prefab)
    {
        if(poolDic.ContainsKey(name))
            return poolDic[name];

        Transform root = new GameObject($"{name} Pool").transform;
        root.parent = parent;
        parentDic.Add(name, root);

        ObjectPool<GameObject> pool = new ObjectPool<GameObject>
        (
            createFunc: () =>
            {
                GameObject obj = Instantiate(prefab);
                obj.name = name;
                obj.transform.parent = root;
                lastUseTimeDic[name] = Time.time;
                return obj;
            },
            actionOnGet: (GameObject go) =>
            {
                go.transform.parent = null;
                go.SetActive(true);
                lastUseTimeDic[name] = Time.time;
            },
            actionOnRelease: (GameObject go) =>
            {
                go.transform.parent = root;
                go.SetActive(false);
            },
            actionOnDestroy: (GameObject go) =>
            {
                Destroy(go);
            },
            maxSize: 10
        );

        poolDic.Add(name, pool);
        return pool;
    }

    public GameObject GetPopup(GameObject go, Vector3 position)
    {
        if (popUpPool == null)
            popUpPool = CreatePopUpTextPool(go);

        GameObject obj = popUpPool.Get();
        obj.transform.position = position;

        return obj;
    }

    public T Get<T> (T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
    {
        GameObject go = original as GameObject;
        string name = go.name;

        var pool = GetOrCreatePool(name, go);

        go = pool.Get();

        if (parent != null)
            go.transform.SetParent(parent, false);

        go.transform.localPosition = position;
        go.transform.rotation = rotation;

        return go as T;
    }

    public T Get<T>(T original, Vector3 position, Quaternion rotation) where T : Object
    {
        return Get<T>(original, position, rotation, null);
    }

    public T Get<T>(T original, Vector3 position) where T : Object
    {
        return Get<T>(original, position, Quaternion.identity);
    }

    public T Get<T>(T original, Vector3 position, Transform parent) where T : Object
    {
        return Get<T>(original, position, Quaternion.identity, parent);
    }

    public void PopupRelease<T> (T original) where T : Object
    {
        GameObject obj = original as GameObject;

        if(obj.activeSelf)
            popUpPool.Release(obj);
    }

    public void Release<T> (T original) where T : Object
    {
        GameObject obj = original as GameObject;
        string name = obj.name;

        if (!poolDic.ContainsKey(name) && !obj.activeSelf)
            return;

        poolDic[name].Release(obj);
    }

    public void Release<T>(T original, float delay) where T : Object
    {
        StartCoroutine(DelayRelease(original, delay));
    }

    private IEnumerator DelayRelease<T>(T original, float delay) where T : Object
    {
        yield return new WaitForSeconds(delay);

        GameObject obj = original as GameObject;
        string name = obj.name;

        if (!poolDic.ContainsKey(name) && !obj.activeSelf)
            yield break;
        Debug.Log(obj.activeSelf);
        poolDic[name].Release(obj);
    }

    public bool ContainsKey(string name) => poolDic.ContainsKey(name);
}
