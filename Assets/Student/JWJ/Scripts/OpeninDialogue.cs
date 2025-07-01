using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class OpeninDialogue : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject startDialogue;

    private void Start()
    {
        panel.SetActive(false);
        startDialogue.SetActive(false);
        dialogueManager.dialogueOver += DialogueOver;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Prolog();
        }
    }
    private void Prolog()
    {
        panel.SetActive(true);
        DialogueData[] lines = new DialogueData[]
        {
            new DialogueData("", "18XX�� XX�� XX��.", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Auto),
            new DialogueData("<color=yellow>???</color>", "�� �� ������ ��..... �̷� ���� ����������...", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Auto),
            new DialogueData("", "������ �� ������ ������ �ñ�.", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Auto),
            new DialogueData("<color=green>???</color>", "��� �� �� ������...", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Auto),
            new DialogueData("", "����޴� �ùε��� ���¢���� ������ �̷�� �Ÿ����� �︮�� �ñ�.", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Auto),
            new DialogueData("<color=red>???</color>", "r�ƾ�.... ���� ������...���̽ÿ�...", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Auto),
            new DialogueData("", "�ŵ����� ������ ���� ȯȣ���� �ұ����� ���� �޿�� �ñ�.", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Auto),
            new DialogueData("<color=red>???</color>", "r�� ������.... �츮�� ���Ӱ� �¾ ���Դϴ�...", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Auto),
            new DialogueData("", "��ȯ�� �Ǹ��� ������ �����ϴ� �ñ�.", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Auto),
            new DialogueData("<color=red>???</color>", "r�����Ǹ���.... �亣���Ը���...\n ����... �츮��... ������ ��ȭ�� ���̴�...", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Auto),
            new DialogueData("", "<color=red>������</color>�� �� ������ ���İ��� �����ϱ� �����ߴ�.", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Auto),
            //���̵�ƿ�ȿ��
        };
        dialogueManager.ShowDialogue(lines, true);
    }

    private void DialogueOver()
    {
        panel.SetActive(false);
        startDialogue.SetActive(true);
        gameObject.SetActive(false);
    }
}
