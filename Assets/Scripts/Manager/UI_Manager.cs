using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : Singleton<UI_Manager>
{
    public Canvas WorldCanvas;
    public Canvas MainCanvas;

    public AccessoriesChangePanel AccessoriesChangePanel;
    public ShopView ShopView;
    public FadeScreen Fade;

    private void Awake()
    {
        WorldCanvas = Instantiate(Resources.Load<Canvas>("UI/WorldCanvas"));
        WorldCanvas.transform.parent = transform;

        MainCanvas = Instantiate(Resources.Load<Canvas>("UI/MainCanvas"));
        MainCanvas.transform.parent = transform;        

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
}