using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAfterDialogue : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    private Image panel;

    private void Awake()
    {
        panel = GetComponent<Image>();
    }
    private void Start()
    {
        dialogueManager.dialogueOver += Fade;
    }
    public void Fade()
    {
        StartCoroutine(FadeOut());
    }
    private IEnumerator FadeOut()
    {
        float duration = 2f;
        float timer = 0f;

        Color startColor = panel.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            panel.color = Color.Lerp(startColor, targetColor, timer / duration);
            yield return null;
        }

        panel.color = targetColor;
        dialogueManager.dialogueOver -= Fade;
        gameObject.SetActive(false);
        
    }
}
