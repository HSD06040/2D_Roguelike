using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    private Dictionary<string, GameObject> uiObjectDic;
    private Dictionary<string, Component> componentDic;

    private void Awake()
    {
        //Canvas의 경우 모두 RectTransform를 가지고있기 때문에 Transform X, Rectransform O - 모든 하위 UI를 가져오겠다.
        RectTransform[] recTrans = GetComponentsInChildren<RectTransform>(true);
        uiObjectDic = new Dictionary<string, GameObject>(recTrans.Length * 4);

        foreach(RectTransform child in recTrans)
        {
            //Add가 아닌 이유 : 동일한 이름의 ui일경우 하나만 가져오기 위해(보통 필요한 ui이름은 따로 지정해주기 때문)
            uiObjectDic.TryAdd(child.gameObject.name, child.gameObject);
        }

        Component[] components = GetComponentsInChildren<Component>(true);
        componentDic = new Dictionary<string, Component>();

        foreach (Component child in components)
        {
            componentDic.TryAdd($"{child.gameObject.name}_{child.GetType().Name}", child);
        }
    }

    public GameObject GetUI(in string objName)
    {
        uiObjectDic.TryGetValue(objName, out GameObject obj);
        return obj;
    }


    public T GetUI<T>(in string objName) where T : Component
    {
        componentDic.TryGetValue($"{objName}_{typeof(T).Name}", out Component component);

        if(component != null)
            return component as T;

        GameObject obj = GetUI(objName);
        if (obj == null)
            return null;

        component = obj.GetComponent<T>();
        if (component == null)
            return null;

        componentDic.TryAdd($"{objName}_{typeof(T).Name}", component);
        return component as T;
    }


    public PointHandler GetEvent(in string objName)
    {
        GameObject obj = GetUI(objName);
        return obj.GetOrAddComponent<PointHandler>();
    }
}
