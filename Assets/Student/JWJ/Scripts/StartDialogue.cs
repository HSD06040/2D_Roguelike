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
            new DialogueData("<color=blue>��</color>", "���� �����ϱ�", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=blue>��</color>", "���ݱ��� ���� ������ �������鼭 �̷��Ա��� ������ ���� ó�� ���µ�..", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=yellow>???</color>", "�����ǻ��.. ������..����.....", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=blue>��</color>", "��ó�� ���� �ʾ� �����̳׿�. �켱 �̷��Ը��̶󵵡�", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
        };
        dialogueManager.ShowDialogue(lines, false);
    }

    private void StartStory2()
    {
        DialogueData[] lines = new DialogueData[]
        {
            new DialogueData("<color=yellow>???</color>", "���� �����մϴ�...", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=blue>��</color>", "��Ȳ�� ���� �ɰ��� ���� �����?", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=yellow>???</color>", "�����̶�� ������ϴ�.. ���� ������ �ſ���..", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=blue>��</color>", "���� ������.", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
        };
        dialogueManager.ShowDialogue(lines, true);
    }

    private IEnumerator WaitForNextStory()
    {
        Debug.Log("ȭ�� ȿ�� ���ľ���");
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