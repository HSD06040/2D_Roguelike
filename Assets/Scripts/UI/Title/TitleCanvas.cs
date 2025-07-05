using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleCanvas : BaseUI
{
    private GameObject pressSpaceButton => GetUI("PressSpaceButton");
    private Button pressGameStartButton => GetUI<Button>("PressGameStartButton");
    private Button pressOptionButton => GetUI<Button>("PressOptionButton");
    private Button pressExitButton => GetUI<Button>("PressExitButton");

    private GameObject titleText => GetUI("TitleText");
    private GameObject pressImageButton1 => GetUI("PressImage1");
    private GameObject pressImageButton2 => GetUI("PressImage2");
    private GameObject pressImageButton3 => GetUI("PressImage3");

    private WaitForSeconds delay = new WaitForSeconds(0.4f);

    private bool isPress;
    private int count;
    private void OnDestroy()
    {
        if(gameObject != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        if (Manager.Audio.bgmSource == null)
            Manager.Audio.bgmSource.clip = Manager.Resources.Load<AudioClip>($"Sound/BGM/Title");

        Manager.Audio.PlayBGM("Title");
        GetEvent("PressOptionButton").Click += data =>
        {
            Manager.UI.ShowPopUp<SettingPopUp>();
        };
        //이부분에 초기화작업 해줘야함
        GetEvent("PressGameStartButton").Click += data =>
        {
            if (Manager.Game.IsDead)
                Manager.Game.IsDead = false;

            Manager.UI.ClosePopUp();
            SceneManager.LoadSceneAsync(1);
        };
        NotPress();
        count++;
    }

    private void NotPress()
    {
        pressSpaceButton.SetActive(true);
        pressGameStartButton.gameObject.SetActive(false);
        pressOptionButton.gameObject.SetActive(false);
        pressExitButton.gameObject.SetActive(false);
        StartCoroutine(PressSpaceTitle());
    }

    IEnumerator PressSpaceTitle()
    {
        while (!isPress)
        {
            yield return delay;
            pressSpaceButton.SetActive(false);
            yield return delay;
            pressSpaceButton.SetActive(true);
        }
        pressSpaceButton.SetActive(false);

        pressGameStartButton.gameObject.SetActive(true);
        pressOptionButton.gameObject.SetActive(true);
        pressExitButton.gameObject.SetActive(true);
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isPress = true;
        }

        Press();
    }



    private void Press()
    {
        GetEvent("PressGameStartButton").Enter += data => { pressImageButton1.SetActive(true); ChangeColor(pressGameStartButton); };
        //GetEvent("PressGameStartButton").Click += data => { SceneManager.LoadSceneAsync(1); }; // Fade추가 or 로딩 추가

        GetEvent("PressOptionButton").Enter += data => { pressImageButton2.SetActive(true); ChangeColor(pressOptionButton); };
        //GetEvent("PressOptionButton").Click += data => { Resources.Load("") or GetEvent("팝업창 띄우기")};

        GetEvent("PressExitButton").Enter += data => { pressImageButton3.SetActive(true); ChangeColor(pressExitButton); };
#if UNITY_EDITOR
        GetEvent("PressExitButton").Click += data => { UnityEditor.EditorApplication.isPlaying = false; };
#else
        GetEvent("PressExitButton").Click += data => { Application.Quit(); };
#endif

        GetEvent("PressGameStartButton").Exit += data => { pressImageButton1.SetActive(false); NormalColor(pressGameStartButton); };
        GetEvent("PressOptionButton").Exit += data => { pressImageButton2.SetActive(false); NormalColor(pressOptionButton); };
        GetEvent("PressExitButton").Exit += data => { pressImageButton3.SetActive(false); NormalColor(pressExitButton); };
    }


    
    private void ChangeColor(Button btn)
    {
        Color highlightedCol = Color.yellow;
        ColorBlock myBtn = btn.colors;

        myBtn.normalColor = highlightedCol;
        btn.colors = myBtn;
    }

    private void NormalColor(Button btn)
    {
        Color highlightedCol = Color.white;
        ColorBlock myBtn = btn.colors;

        myBtn.normalColor = highlightedCol;
        btn.colors = myBtn;
    }
}
