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
            new DialogueData("", "18XX년 XX월 XX일.", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Auto),
            new DialogueData("<color=yellow>???</color>", "왜 그 음악을 들어서..... 이런 일이 벌어졌을까...", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Auto),
            new DialogueData("", "음악이 이 세상의 전부인 시기.", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Auto),
            new DialogueData("<color=green>???</color>", "모든 게 다 끝났어...", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Auto),
            new DialogueData("", "고통받는 시민들의 울부짖음이 불협을 이루며 거리마다 울리던 시기.", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Auto),
            new DialogueData("<color=red>???</color>", "r아아.... 나를 구원할...신이시여...", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Auto),
            new DialogueData("", "신도들의 찢어질 듯한 환호성이 소극장을 가득 메우던 시기.", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Auto),
            new DialogueData("<color=red>???</color>", "r이 선율로.... 우리는 새롭게 태어날 것입니다...", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Auto),
            new DialogueData("", "소환된 악마가 세상을 지배하던 시기.", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Auto),
            new DialogueData("<color=red>???</color>", "r흑음악만이.... 토베벤님만이...\n 나를... 우리를... 세상을 정화할 것이니...", DialoguePosition.Bottom, DialogueEffect.Typing, DialogueAdvanceType.Auto),
            new DialogueData("", "<color=red>흑음악</color>은 이 세상을 순식간에 지배하기 시작했다.", DialoguePosition.Center, DialogueEffect.FadeIn, DialogueAdvanceType.Auto),
            //페이드아웃효과
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
