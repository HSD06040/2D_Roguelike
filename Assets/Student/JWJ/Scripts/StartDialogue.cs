using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;

    private bool hasPlayedStory2 = false;

    private void Start()
    {
        StartStory1();
        dialogueManager.dialogueOver += DialogueOver;
    }

    private void StartStory1()
    {
        DialogueData[] lines = new DialogueData[]
        {
            new DialogueData("<color=blue>나</color>", "정말 끔찍하군", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=blue>나</color>", "지금까지 많은 마을을 지나오면서 이렇게까지 망가진 곳은 처음 보는데..", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=yellow>???</color>", "백음악사님.. 도와주..세요.....", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=blue>나</color>", "상처가 깊지 않아 다행이네요. 우선 이렇게만이라도…", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
        };
        dialogueManager.ShowDialogue(lines, false);
    }

    private void StartStory2()
    {
        DialogueData[] lines = new DialogueData[]
        {
            new DialogueData("<color=yellow>???</color>", "정말 감사합니다...", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=blue>나</color>", "상황이 제일 심각한 곳이 어디죠?", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=yellow>???</color>", "광장이라고 들었습니다.. 많이 위험할 거예요..", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=blue>나</color>", "걱정 마세요.", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
        };
        dialogueManager.ShowDialogue(lines, true);
    }

    private IEnumerator WaitForNextStory()
    {
        Debug.Log("화면 효과 고쳐야함");
        //Manager.UI.Fade.PlayFade(1f, 1f);
        yield return new WaitForSeconds(3);
        StartStory2();
    }

    private void DialogueOver()
    {
        if(!hasPlayedStory2)
        {
            hasPlayedStory2 = true;
            StartCoroutine(WaitForNextStory());
        }
        else
        {

        }    
        
    }
}