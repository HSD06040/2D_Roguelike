using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpen : MonoBehaviour, IInteractable
{
    //[SerializeField] private GameObject shopUI;
    [SerializeField] GameObject interectionUI;

    private ShopPresenter shopPresenter;
    private ShopView shopView;
    private Animator animator;

    private bool hasOpended = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        shopView = Manager.UI.ShopView;
        interectionUI.SetActive(false);
    }
    public void Interact()
    {
        if (!hasOpended)
        {
            hasOpended = true;
            animator.SetTrigger("Open");
            Debug.Log("»óÀÚ¿­¸²");
            UiOff();

            ShopModel shopModel = new ShopModel();
            shopPresenter = new ShopPresenter(shopView);

            shopView._Start(shopPresenter);
            shopPresenter.ShopSetting();
            shopView.Open();

            Debug.Log("¼¥¿ÀÇÂ");
        }
    }

    public void UiOn()
    {
        if (!hasOpended)
        {
            interectionUI.SetActive(true);
        }
    }

    public void UiOff()
    {
        interectionUI.SetActive(false);
    }

}
