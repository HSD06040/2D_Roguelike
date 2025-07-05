using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject interectionUI;

    private bool isActivated;

    private void Start()
    {
        isActivated = false;
    }
    public void Interact()
    {
        if(!isActivated)
        {
            isActivated=true;
            StartCoroutine(ScreenFade());
        }
        
    }

    private IEnumerator ScreenFade()
    {
        int currentSenen = SceneManager.GetActiveScene().buildIndex;
        Manager.UI.Fade.PlayFade(1f, 2f);  //페이드 효과
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync(currentSenen + 1);
        Debug.Log("다음씬 이동");
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
