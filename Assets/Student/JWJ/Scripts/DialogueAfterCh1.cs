using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueAfterCh1 : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    private GameObject player;

    private void Start()
    {
        DialogueAfterChapter1();
        Manager.Audio.PlayBGM("Chapter2/StageChapter2");/////////////
        player = GameObject.FindWithTag("Player");
        player.SetActive(false);
    }

    public void DialogueAfterChapter1()
    {
        dialogueManager.dialogueOver += DialogueOver;
        PrintLines();
    }

    private void PrintLines()
    {
        DialogueData[] lines = new DialogueData[]
        {
            new DialogueData("", "시민들의 울음소리와 비명소리는 점차 줄어들고 있었다.", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("", "도움을 받은 사람들은 내게 고개 숙이며 감사를 표했다.", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("", "시민들은 자신들이 이렇게 된 이유가\nr소극장에서 들리는 연주 소리 w때문이라고 했다.", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),
            new DialogueData("", "도대체 무슨 일이 벌어지고 있는 걸까.", DialoguePosition.Center, DialogueEffect.Typing, DialogueAdvanceType.Manual),

        };
        dialogueManager.ShowDialogue(lines, false);
    }

    private void DialogueOver()
    {
        dialogueManager.dialogueOver -= DialogueOver;
        player.SetActive(true);
    }
   
}
