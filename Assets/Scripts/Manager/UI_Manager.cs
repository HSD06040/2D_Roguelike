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
    public BoxRewardUI BoxReward;
    public StatusPopUp StatusView;

    //맨처음 스페이스바
    //public Property<bool> OnPress = new();
    private void Awake()
    {
        InitUI();
    }

    public void ResetUI()
    {
        MainCanvas.gameObject.SetActive(false);
        MainCanvas.gameObject.SetActive(true);
    }   

    private void InitUI()
    {
        WorldCanvas = Instantiate(Resources.Load<Canvas>("UI/WorldCanvas"));
        WorldCanvas.transform.parent = transform;

        MainCanvas = Instantiate(Resources.Load<Canvas>("UI/MainCanvas"));
        MainCanvas.transform.parent = transform;

        AccessoriesChangePanel = MainCanvas.GetComponentInChildren<AccessoriesChangePanel>(true);
        ShopView = MainCanvas.GetComponentInChildren<ShopView>(true);
        PopupText = MainCanvas.GetComponentInChildren<PopupText>(true);
        BoxReward = MainCanvas.GetComponentInChildren<BoxRewardUI>(true);
        StatusView = MainCanvas.GetComponentInChildren<StatusPopUp>(true);

        Canvas fadeCanvas = Instantiate(Resources.Load<Canvas>("UI/FadeCanvas"));
        fadeCanvas.transform.parent = transform;
        Fade = fadeCanvas.GetComponentInChildren<FadeScreen>(true);

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

    public void ShowTitle()
    {
        PopUpCanvas.GetComponent<PopUpCanvas>().Showtitle();
    }
}

