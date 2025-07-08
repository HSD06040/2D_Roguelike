using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class DialogueBoss : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] GameObject panel;
    private bool hasSeenFinal = false;
    private bool isEnding = false;
    [SerializeField] private GameObject theEndButton;
    private GameObject player => GameObject.FindWithTag("Player");

    public void DialogueFinal()
    {
        dialogueManager.dialogueOver += DialogueOver;
        Destroy(player);
        PrintLines();
    }

    private void PrintLines()
    {
        DialogueData[] lines = new DialogueData[]
        {
            new DialogueData("<color=red>토베벤</color>", "r하..하하하... 아하하하하하!!!!!!!!w", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData(" ", "귀가 찢어질 듯한 웃음소리가 귀에 박혔다.", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=red>토베벤</color>", "r이게 끝일 거라고 생각하나 백음악사!!!\n난 다시 돌아올 것이다!!w", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("", "모든 불협화음이 모여 언어가 된 듯. 듣기 싫은 말들이 들려왔다.", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=red>토베벤</color>", "r이 세상을 지배할 것이란 말이ㄷw", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Auto),
        };
        dialogueManager.ShowDialogue(lines, false);
    }

    private void PrintFinalLines()
    {
        DialogueData[] lines = new DialogueData[]
        {
            new DialogueData("", "참을 수 없는 분노를 억누르며 토베벤을 정화시켰다.", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData(" ", "그러자, 오케스트라장은\n흑음악 공연이 전부 끝났음을 알리는 것처럼 정적만이 흘렀다.", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("", "<color=#87CEEB>이제, 내가 백음악을 연주할 차례다.</color>", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Manual),
        };
        dialogueManager.ShowDialogue(lines, false);
    }

    private void FinalDialogue()
    {
        dialogueManager.dialogueOver += DialogueOver;
        PrintFinalLines();
    }

    private void DialogueOver()
    {
        dialogueManager.dialogueOver -= DialogueOver;
        if(!hasSeenFinal && !isEnding)
        {
            hasSeenFinal = true;
            StartCoroutine(Effect());
        }
        if(isEnding)
        {
            StartCoroutine(FinalEffect());
        }
    }

    private IEnumerator Effect()
    {
        Manager.UI.Fade.PlayFade(1f, 1f, Color.white);
        yield return new WaitForSeconds(2);
        panel.SetActive(true);
        FinalDialogue();
        isEnding = true;
    }

    private IEnumerator FinalEffect()
    {
        Manager.UI.Fade.PlayFade(2f, 2f, Color.white);
        yield return new WaitForSeconds(6.5f);
        theEndButton.SetActive(true);
    }

    public void OnTheEndButtonPressed()
    {
        //theEndButton.SetActive(false);
        //panel.SetActive(false);
        SceneManager.LoadScene("TitleScene");
        //Manager.UI.ShowPopUp<TitleCanvas>();
    }

}
