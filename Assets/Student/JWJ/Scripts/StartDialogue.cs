using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Prolog();
        }
    }
    private void Prolog()
    {
        Debug.Log("���ѷα� ����");
        DialogueData[] lines = new DialogueData[]
        {
            new DialogueData("", "18XX�� XX�� XX��.", DialoguePosition.Center),
            new DialogueData("<color=yellow>???</color> ", "�� �� ������ ��..... �̷� ���� ����������...", DialoguePosition.Bottom),
            new DialogueData("", "������ �� ������ ������ �ñ�.", DialoguePosition.Center),
            new DialogueData("<color=green>???</color>", "��� �� �� ������...", DialoguePosition.Bottom),
            new DialogueData("", "����޴� �ùε��� ���¢���� ������ �̷�� �Ÿ����� �︮�� �ñ�.", DialoguePosition.Center),
            new DialogueData("<color=red>???</color>", "<color=red>�ƾ�.... ���� ������...���̽ÿ�...</color>", DialoguePosition.Bottom),
            new DialogueData("", "�ŵ����� ������ ���� ȯȣ���� �ұ����� ���� �޿�� �ñ�.", DialoguePosition.Center),
            new DialogueData("<color=red>???</color>", "<color=red>�� ������.... �츮�� ���Ӱ� �¾ ���Դϴ�...</color>", DialoguePosition.Bottom),
            new DialogueData("", "��ȯ�� �Ǹ��� ������ �����ϴ� �ñ�.", DialoguePosition.Center),
            new DialogueData("<color=red>???</color>", "<color=red>�����Ǹ���.... �亣���Ը���...\n ����... �츮��... ������ ��ȭ�� ���̴�...</color>", DialoguePosition.Bottom),
            new DialogueData("", "<color=red>������</color>�� �� ������ ���İ��� �����ϱ� �����ߴ�.", DialoguePosition.Center),
            //���̵�ƿ�ȿ��
        };

        dialogueManager.ShowDialogue(lines, DialogueEffect.FadeIn, DialogueAdvanceType.Auto, true);
    }

   
}