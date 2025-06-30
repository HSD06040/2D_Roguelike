using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public enum DialoguePosition { Center, Bottom }

public enum DialogueEffect { FadeIn, Typing }

public enum DialogueAdvanceType{ Auto, Manual }

public class DialogueData
{
    public string dialogue; //��� ����
    public DialoguePosition position; // ����� ��ġ
    public string speaker;

    public DialogueData(string speaker, string line, DialoguePosition position) //������, ���̾�α� ����� ����� �����͵�
    {
        this.dialogue = line;
        this.position = position;
        this.speaker = speaker;
    }
}

public class DialogueManager : MonoBehaviour//Singleton<DialogueManager>
{
    [Header("UI �г�")]
    [SerializeField] private GameObject centerPanel;
    [SerializeField] private GameObject bottomPanel;

    [Header("�̸� �ؽ�Ʈ")]
    [SerializeField] private TMP_Text nameText;

    [Header("��� �ؽ�Ʈ")]
    [SerializeField] private TMP_Text centerDialogueText;
    [SerializeField] private TMP_Text bottomDialogueText;

    [Header("ȿ�� ����")]
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float autoAdvanceDelay = 2f;

    [Header("��ŵ��ư")]
    [SerializeField] private GameObject skipButton;


    private TMP_Text currentText;
    private DialogueEffect curEffect;
    private DialogueAdvanceType curAdvanceType;
    private bool isSkipable = false;
    private bool isWaitingForInput = false;

    private Coroutine effectCoroutine;
    private Coroutine autoCoroutine;

    public event Action dialogueOver;

    Queue<DialogueData> sentences = new Queue<DialogueData>();

    private void Awake()
    {
        skipButton.GetComponent<Button>().onClick.AddListener(OnSkipbuttonClicked);
        skipButton.SetActive(false);
    }

    private void Update()
    {
        if (isWaitingForInput)
        {
            InputToNextSentence();
        }
    }

    /// <summary>
    /// ��縦 ����ϰ� ����� ��ġ, ȿ��, ������, ��ŵ���θ� �����մϴ�.
    /// </summary>
    /// <param name="lines">���� ���, �̸�, ����, ������ ����</param>
    /// <param name="effect">���� ��� ȿ��(Fade In, Typing)</param>
    /// <param name="advanceType">��� �ѱ� ���(Auto, Manual)</param>
    /// <param name="canSkip">��ü ��ŵ ���� ���� (true, false)</param>
    public void ShowDialogue(DialogueData[] lines, DialogueEffect effect, DialogueAdvanceType advanceType, bool canSkip)
    {
        sentences.Clear(); //ť ���

        foreach(DialogueData line in lines)
        {
            sentences.Enqueue(line);
        }

        curEffect = effect; //���� ����Ʈ
        curAdvanceType = advanceType; //���� Ÿ��
        isSkipable = canSkip; //��ŵ���ɿ���
        skipButton.SetActive(isSkipable);  //��ŵ �����̸� ��ŵ��ư Ȱ��ȭ

        DisplaySentence(); //��� ���
    }

    public void DisplaySentence()
    {
        if(sentences.Count <= 0)  //����Ұ� ���̻� ������
        {
            //DialogueOver();
            return;
        }

        if (effectCoroutine != null)  //�ڷ�ƾ�� ������ ������
        {
            StopCoroutine(effectCoroutine);
            effectCoroutine = null;
        }

        if (autoCoroutine != null)
        {
            StopCoroutine(autoCoroutine);
            autoCoroutine = null;
        }

        DialogueData line = sentences.Dequeue();
        //nameText.text = line.speaker;

        if (line.position == DialoguePosition.Center)
        {
            centerPanel.SetActive(true);
            bottomPanel.SetActive(false);
            currentText = centerDialogueText;
        }
        else
        {
            centerPanel.SetActive(false);
            bottomPanel.SetActive(true);
            currentText = bottomDialogueText;

            
        }

        if(curEffect == DialogueEffect.FadeIn)  //����Ʈ�� ���̵����̶��
        {
            effectCoroutine = StartCoroutine(TextFadeIn(line.dialogue, line.speaker));
        }

        else if(curEffect == DialogueEffect.Typing) //����Ʈ�� Ÿ�����̶��
        {
            effectCoroutine = StartCoroutine(TextTyping(line.dialogue));
        }
    }

    private IEnumerator TextFadeIn(string line, string speaker)  //���̵��� ȿ�� �ڷ�ƾ
    {
        nameText.text = speaker;
        Color nameColor = nameText.color;
        nameColor.a = 0;
        nameText.color = nameColor;

        currentText.text = line;
        Color color = currentText.color;  // �����ؽ�Ʈ�� �÷��� �����
        color.a = 0; //������ 0���� ����
        currentText.color = color; //����

        float timer = 0f;
        while (timer < fadeInDuration)  //Ÿ�̸Ӱ� �෹�̼�Ÿ���� �Ǵµ���
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInDuration); //���Ķ�� ������ �����ϰ� 0���� 1���� �÷���
            nameText.color = new Color(nameColor.r, nameColor.g, nameColor.b, alpha);
            currentText.color = new Color(color.r, color.g, color.b, alpha); //���� ���ĸ� ���� ���Ŀ� ����
            yield return null;
        }
        color.a = 1f;  // ���Ϲ��� ������ ���ĸ� 1�� Ȯ���� ��������
        nameColor.a = 1f;

        nameText.color = nameColor;
        currentText.color = color;  //����

        SelectAdvanceType();
    }

    private IEnumerator TextTyping(string line) //Ÿ���� ȿ��
    {
        currentText.text = ""; //�ѱ��ھ� ���;� �ϴϱ� ��ť�� ������ �ʱ�ȭ����

        foreach(char letter in line)
        {
            currentText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        SelectAdvanceType();
    }

    private void SelectAdvanceType()
    {
        if (curAdvanceType == DialogueAdvanceType.Auto)  //��� ���� Ÿ���� ������
        {
            StartCoroutine(AutoNextSentence());
        }
        else if (curAdvanceType == DialogueAdvanceType.Manual) //���� Ÿ���� �޴����̸�
        {
            isWaitingForInput = true;
        }
    }

    private IEnumerator AutoNextSentence()  //�ڵ����� �����ؽ�Ʈ�� �Ѿ (���̵�ƿ�)
    {
        Color nameColor = nameText.color;

        Color color = currentText.color;
        float timer = 0f;
        while (timer < autoAdvanceDelay)  //���̵�ƿ����� �����
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / autoAdvanceDelay); 
            currentText.color = new Color(color.r, color.g, color.b, alpha); 
            nameText.color = new Color(nameColor.r, nameColor.g, nameColor.b, alpha);
            yield return null;
        }

        if (sentences.Count <= 0)
        {
            DialogueOver();
            dialogueOver?.Invoke();
        }
        else
        {
            DisplaySentence();
        }  
    }

    private void InputToNextSentence() // ��ǲ�� �־�� ���� �ؽ�Ʈ�� �Ѿ
    {
        if(Input.anyKeyDown)
        {
            DisplaySentence();
            isWaitingForInput = false;
        }
    }

    private void OnSkipbuttonClicked() //��ŵ��ư ������
    {
        StopAllCoroutines();
        DialogueOver();

        dialogueOver?.Invoke();
    }

    private void DialogueOver() //���̾�α� ������
    {
        sentences.Clear();
        centerPanel.SetActive(false);
        bottomPanel.SetActive(false);
        skipButton.SetActive(false);
        isWaitingForInput = false;
    }
}
