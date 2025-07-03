using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interectionUI;
    [SerializeField] private Transform playerStransform;
    [SerializeField] private Transform moveToPos;

    private void Start()
    {
        interectionUI.SetActive(false);
        playerStransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Interact()
    {
        playerStransform.position = moveToPos.position;
    }

    public void UiOn()
    {
        interectionUI.SetActive(true);
    }

    public void UiOff()
    {
        interectionUI.SetActive(false);
    }
}
