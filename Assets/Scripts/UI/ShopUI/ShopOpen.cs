using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpen : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interectionUI;

    private ShopPresenter shopPresenter;
    private ShopView shopView;
    private Animator animator;
    private ShopModel shopModel;

    private bool hasOpended = false;
    private bool hasPurchased = false;

    private void Start()
    {
        interectionUI.SetActive(false);
        animator = GetComponent<Animator>();
        shopView = Manager.UI.ShopView;
        shopModel = new ShopModel();
        shopPresenter = new ShopPresenter(shopView);

        shopView.CloseButtonClicked += BoxClose;
        shopPresenter.Purchased += Purchased;
    }
    public void Interact()
    {
        if (!hasOpended)
        {
            hasOpended = true;
            animator.SetTrigger("Open");
            //Debug.Log("상자열림");
            Manager.Data.AddGold(1000); //테스트용
            //Debug.Log("잔액:" + Manager.Data.Gold.Value);//테스트용
            UiOff();

            
            
            shopView._Start(shopPresenter);
            shopPresenter.ShopSetting();
            shopView.Open();

            //Debug.Log("샵오픈");
        }

        if (hasOpended && !hasPurchased)
        {
            animator.SetTrigger("Open");
            UiOff();
            shopView.Open();
        }
    }

    public void BoxClose()
    {
        animator.SetTrigger("Close");
        UiOn();

    }

    private void Purchased()
    {
        hasPurchased = true;
        shopView.CloseButtonClicked -= BoxClose;
    }

    public void UiOn()
    {
        if (!hasOpended || !hasPurchased)
        {
            interectionUI.SetActive(true);
        }
    }

    public void UiOff()
    {
        interectionUI.SetActive(false);
    }

}
