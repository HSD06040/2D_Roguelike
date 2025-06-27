using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Box : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interectionUI;

    private Animator animator;
    private bool hasOpended = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (!hasOpended)
        {     
            hasOpended = true;
            animator.SetTrigger("Open");
            Debug.Log("���ڿ���");
            UiOff();
            //�÷��̾�� �������ְų� �������� ���
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
