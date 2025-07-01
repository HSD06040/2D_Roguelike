using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : Singleton<UI_Manager>
{
    public Canvas WorldCanvas;
    public Canvas MainCanvas;
    public Canvas PopUpCanvas;

    public AccessoriesChangePanel AccessoriesChangePanel;
    public ShopView ShopView;
    public FadeScreen Fade;

    private void Awake()
    {
        WorldCanvas = Instantiate(Resources.Load<Canvas>("UI/WorldCanvas"));
        WorldCanvas.transform.parent = transform;

        MainCanvas = Instantiate(Resources.Load<Canvas>("UI/MainCanvas"));
        MainCanvas.transform.parent = transform;

        PopUpCanvas = Instantiate(Resources.Load<Canvas>("UI/PopUpCanvas"));
        PopUpCanvas.transform.parent = transform;

        AccessoriesChangePanel = MainCanvas.GetComponentInChildren<AccessoriesChangePanel>(true);
        ShopView = MainCanvas.GetComponentInChildren<ShopView>(true);
        Fade = MainCanvas.GetComponentInChildren<FadeScreen>(true);
    }

    public void OpenShop()
    {
        ShopView.Open();
    }

    public void OpenAccessoriesChangepanel(Accessories ac)
    {
        AccessoriesChangePanel.OpenChangePanel(ac);
    }

    public T ShowPopUp<T>() where T : BaseUI //��ũ��Ʈ �̸��� UI�̸� �����ؾ���
    {
        T prefab = Resources.Load<T>($"UI/PopUpUI/{typeof(T).Name}");
        T instance = Instantiate(prefab, PopUpCanvas.transform);
        PopUpCanvas.GetComponent<PopUpCanvas>().AddUI(instance);

        return instance;
    }

    public void ClosePopUp()
    {
        PopUpCanvas.GetComponent<PopUpCanvas>().RemoveUI();
    }
}

