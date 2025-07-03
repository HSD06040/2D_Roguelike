using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interectionUI;

    public void Interact()
    {
        StartCoroutine(ScreenFade());
    }

    private IEnumerator ScreenFade()
    {
        int currentSenen = SceneManager.GetActiveScene().buildIndex;
        Manager.UI.Fade.PlayFade(1f, 1f);  //���̵� ȿ��
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(currentSenen + 1);
        Debug.Log("������ �̵�");

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
