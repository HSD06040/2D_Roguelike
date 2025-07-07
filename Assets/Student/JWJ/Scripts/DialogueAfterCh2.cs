using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAfterCh2 : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    private GameObject player;

    private void Start()
    {
        DialogueAfterChapter2();
        Manager.Audio.PlayBGM("Chapter3/StageChapter3");/////////////
        player = GameObject.FindWithTag("Player");
        player.SetActive(false);
    }

    public void DialogueAfterChapter2()
    {
        dialogueManager.dialogueOver += DialogueOver;
        PrintLines();
    }

    private void PrintLines()
    {
        DialogueData[] lines = new DialogueData[]
        {
            new DialogueData("", "소극장을 가득 매우던 광신도들의 환호성은 사라졌고.", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("", "흑음악이라 불리던 연주 소리는 처음부터 없었다는 듯 고요함만이 나를 에워쌌다.", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("", "세뇌에서 벗어난 신도는 나에게 울면서 고해성사를 하였다.", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("", "이 모든건 오케스트라장에 있는 악마가 벌인 일이라고..", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("", "난 다짐했다.", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("", "r그 악마를, 흑음악을, 이 세상을 백음악으로 정화하리라.w", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),

        };
        dialogueManager.ShowDialogue(lines, false);
    }

    private void DialogueOver()
    {
        dialogueManager.dialogueOver -= DialogueOver;
        player.SetActive(true);
    }
}
