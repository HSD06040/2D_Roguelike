using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoss : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;

    public void DialogueFinal()
    {
        dialogueManager.dialogueOver += DialogueOver;
        PrintLines();
    }

    private void PrintLines()
    {
        DialogueData[] lines = new DialogueData[]
        {
            new DialogueData("<color=red>토베벤</color>", "r하..하하하... 아하하하하하!!!!!!!!w", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData(" ", "귀가 찢어질 듯한 웃음소리가 귀에 박혔다.", DialoguePosition.Bottom, DialogueEffect.FadeIn, DialogueAdvanceType.Manual),
            new DialogueData("<color=red>토베벤</color>", "r이게 끝일 거라고 생각하나 백음악사!!!\n난 다시 돌아올 것이다!!w", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("", "모든 불협화음이 모여 언어가 된 듯. 듣기 싫은 말들이 들려왔다.", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("<color=red>토베벤</color>", "r이 세상을 지배할 것이란 말이ㄷw", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Auto),
        };
        dialogueManager.ShowDialogue(lines, false);
    }

    private void DialogueOver()
    {
        dialogueManager.dialogueOver -= DialogueOver;
    }

}
