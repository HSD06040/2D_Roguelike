using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private float fadeInDuration = 0.5f;


    Queue<string> sentences = new Queue<string>();



    public void ShowDoalogue (string speaker, string[] lines)
    {
        sentences.Clear();
        dialogueUI.SetActive (true);
        nameText.text = speaker;

        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }

        DisPlayNextSentence();
    }

    public void DisPlayNextSentence()
    {
        if(sentences.Count <= 0)
        {
            dialogueUI.SetActive(false);
            return;
        }
        string line = sentences.Dequeue();
        StartCoroutine("TextFadeIn");
        

    }

    private IEnumerator TextFadeIn(string line)
    {
        dialogueText.text = line;
        Color ogColor = dialogueText.color;
        dialogueText.color = ogColor;
        ogColor.a = 0;

        float timer = 0f;

        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
        }


        yield return null;
    }



}
