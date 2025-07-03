using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Manager : Singleton<UI_Manager>
{
    public Canvas WorldCanvas;
    public Canvas MainCanvas;
    public Canvas PopUpCanvas;

    public AccessoriesChangePanel AccessoriesChangePanel;
    public ShopView ShopView;
    public FadeScreen Fade;
    public PopupText PopupText;
    public PlayerDie PlayerDie;
    public BoxRewardUI BoxReward;

    private void Awake()
    {
        WorldCanvas = Instantiate(Resources.Load<Canvas>("UI/WorldCanvas"));
        WorldCanvas.transform.parent = transform;

        MainCanvas = Instantiate(Resources.Load<Canvas>("UI/MainCanvas"));
        MainCanvas.transform.parent = transform;

        AccessoriesChangePanel = MainCanvas.GetComponentInChildren<AccessoriesChangePanel>(true);
        ShopView = MainCanvas.GetComponentInChildren<ShopView>(true);
        Fade = MainCanvas.GetComponentInChildren<FadeScreen>(true);
        PopupText = MainCanvas.GetComponentInChildren<PopupText>(true);
        BoxReward = MainCanvas.GetComponentInChildren<BoxRewardUI>(true);

        PopUpCanvas = Instantiate(Resources.Load<Canvas>("UI/PopUpCanvas"));
        PopUpCanvas.transform.parent = transform;
        PopUpCanvas.GetOrAddComponent<PopUpCanvas>();
    }

    public void OpenShop()
    {
        ShopView.Open();
    }

    public void OpenAccessoriesChangepanel(Accessories ac)
    {
        AccessoriesChangePanel.OpenChangePanel(ac);
    }

    public void OpenDieMessage()
    {
        PlayerDie.gameObject.SetActive(true);
    }

    public T ShowPopUp<T>() where T : BaseUI //스크립트 이름과 UI이름 동일해야함
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

