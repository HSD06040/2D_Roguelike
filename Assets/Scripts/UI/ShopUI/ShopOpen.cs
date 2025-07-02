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
            //Debug.Log("���ڿ���");
            Manager.Data.AddGold(1000); //�׽�Ʈ��
            //Debug.Log("�ܾ�:" + Manager.Data.Gold.Value);//�׽�Ʈ��
            UiOff();

            
            
            shopView._Start(shopPresenter);
            shopPresenter.ShopSetting();
            shopView.Open();

            //Debug.Log("������");
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
