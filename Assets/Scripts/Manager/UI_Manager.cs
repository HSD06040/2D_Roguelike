using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    public GameObject InfoPanel;
    public GameObject InGamePanel;

    //맨처음 스페이스바
    //public Property<bool> OnPress = new();
    private void Awake()
    {
        InitUI();
        SetupUIBind();
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

        Transform infoPanelTransform = MainCanvas.transform.Find("InfoPanel");
        if (infoPanelTransform != null)
            InfoPanel = infoPanelTransform.gameObject;

        Transform ingamePanelTransform = MainCanvas.transform.Find("InGame");
        if (ingamePanelTransform != null)
            InGamePanel = ingamePanelTransform.gameObject;

        Canvas fadeCanvas = Instantiate(Resources.Load<Canvas>("UI/FadeCanvas"));
        fadeCanvas.transform.parent = transform;
        Fade = fadeCanvas.GetComponentInChildren<FadeScreen>(true);

        PopUpCanvas = Instantiate(Resources.Load<Canvas>("UI/PopUpCanvas"));
        PopUpCanvas.transform.parent = transform;
        PopUpCanvas.GetOrAddComponent<PopUpCanvas>();

        InGamePanel.SetActive(false);
    }

    private void SetupUIBind()
    {        
        Manager.Input.GetUIBind("Info").AddStartedEvent(InfoPanelChange);
    }

    private void InfoPanelChange(InputAction.CallbackContext ctx)
    {
        if(InfoPanel.activeSelf)
        {
            InfoPanel.SetActive(false);

            if(Manager.Game.currentGameState == GameState.InGame)
                Manager.Input.ChangeCursor(CursorType.Attack);
        }
        else
        {
            InfoPanel.SetActive(true);
            Manager.Input.ChangeCursor(CursorType.Defualt);
        }
    }

    public void OpenShop()
    {
        ShopView.Open();
        Manager.Input.ChangeCursor(CursorType.Defualt);
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
        Manager.Input.ChangeCursor(CursorType.Defualt);
        return instance;
    }

    public void ClosePopUp()
    {
        PopUpCanvas.GetComponent<PopUpCanvas>().RemoveUI();

        if (Manager.Game.currentGameState == GameState.InGame)
            Manager.Input.ChangeCursor(CursorType.Attack);
    }

    public void ShowTitle()
    {
        PopUpCanvas.GetComponent<PopUpCanvas>().Showtitle();
        Manager.Input.ChangeCursor(CursorType.Defualt);
    }
}

