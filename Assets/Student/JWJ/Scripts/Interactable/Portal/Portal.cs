using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interectionUI;

    public void Interact()
    {
        //SceneManager.LoadScene("æ¿ ¿Ã∏ß");
        Debug.Log("¥Ÿ¿Ωæ¿ ¿Ãµø");
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
