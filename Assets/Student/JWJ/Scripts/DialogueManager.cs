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
    public string speaker; //���ϴ� �ι� �̸�
    public DialogueEffect effect; // Fade In �̳� Typing
    public DialogueAdvanceType advanceType; // ��ǲ���� �ѱ��� �ڵ����� �ѱ���

    public DialogueData(string speaker, string line, DialoguePosition position, DialogueEffect effect, DialogueAdvanceType advanceType) //������, ���̾�α� ����� ����� �����͵�
    {
        this.dialogue = line;
        this.position = position;
        this.speaker = speaker;
        this.effect = effect;
        this.advanceType = advanceType;
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
    [SerializeField] private float fadeOutDuration = 2f;
    [SerializeField] private float typingRemovalTime = 2f; 

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
    public void ShowDialogue(DialogueData[] lines, bool canSkip)
    {
        sentences.Clear(); //ť ���

        foreach(DialogueData line in lines)
        {
            sentences.Enqueue(line);
        }

        isSkipable = canSkip; //��ŵ���ɿ���
        skipButton.SetActive(isSkipable);  //��ŵ �����̸� ��ŵ��ư Ȱ��ȭ

        DisplaySentence(); //��� ���
    }

    public void DisplaySentence()
    {
        if(sentences.Count <= 0)  //����Ұ� ���̻� ������
        {
            dialogueOver?.Invoke();
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
        
        curEffect = line.effect;
        curAdvanceType = line.advanceType;

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
            effectCoroutine = StartCoroutine(TextFadeIn(line.dialogue, line.speaker, line.advanceType));
        }

        else if(curEffect == DialogueEffect.Typing) //����Ʈ�� Ÿ�����̶��
        {
            effectCoroutine = StartCoroutine(TextTyping(line.dialogue, line.speaker, line.advanceType));
        }
    }

    private IEnumerator TextFadeIn(string line, string speaker, DialogueAdvanceType advanceType)  //���̵��� ȿ�� �ڷ�ƾ
    {
        nameText.text = speaker;
        //Color nameColor = nameText.color;
        //nameColor.a = 0;
        //nameText.color = nameColor;

        currentText.text = line;
        Color color = currentText.color;  // �����ؽ�Ʈ�� �÷��� �����
        color.a = 0; //������ 0���� ����
        currentText.color = color; //����

        float timer = 0f;
        while (timer < fadeInDuration)  //Ÿ�̸Ӱ� �෹�̼�Ÿ���� �Ǵµ���
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInDuration); //���Ķ�� ������ �����ϰ� 0���� 1���� �÷���
            //nameText.color = new Color(nameColor.r, nameColor.g, nameColor.b, alpha);
            currentText.color = new Color(color.r, color.g, color.b, alpha); //���� ���ĸ� ���� ���Ŀ� ����
            yield return null;
        }
        color.a = 1f;  // ���Ϲ��� ������ ���ĸ� 1�� Ȯ���� ��������
        //nameColor.a = 1f;

        //nameText.color = nameColor;
        currentText.color = color;  //����

        if (advanceType == DialogueAdvanceType.Auto)
        {
            StartCoroutine(AutoNextSentence());
        }
        else
        {
            isWaitingForInput = true;
        }

    }

    private IEnumerator TextTyping(string line, string speaker, DialogueAdvanceType advanceType) //Ÿ���� ȿ��
    {
        nameText.text = speaker;

        currentText.text = ""; //�ѱ��ھ� ���;� �ϴϱ� ��ť�� ������ �ʱ�ȭ����

        string displayText = "";
        string curColor = "white"; //�⺻����

        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == 'r') { curColor = "red"; continue; }
            if (line[i] == 'y') { curColor = "yellow"; continue; }
            if (line[i] == 'b') { curColor = "blue"; continue; }
            if (line[i] == 'g') { curColor = "green"; continue; }
            if (line[i] == 'w') { curColor = "white"; continue; }
        
            displayText += $"<color={curColor}>{line[i]}</color>";
            currentText.text = displayText;
        
            yield return new WaitForSeconds(typingSpeed);
        
        }

       //foreach(char letter in line)
       //{
       //    currentText.text += letter;
       //    yield return new WaitForSeconds(typingSpeed);
       //}

        SelectAdvanceType(advanceType);
    }

    private void SelectAdvanceType(DialogueAdvanceType advanceType)
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

    private IEnumerator AutoNextSentence()  //�ڵ����� �����ؽ�Ʈ�� �Ѿ
    {
        if (curEffect == DialogueEffect.FadeIn)
        {
            //Color nameColor = nameText.color;
            Color color = currentText.color;
            float timer = 0f;
            while (timer < fadeOutDuration)  //���̵�ƿ����� �����
            {
                timer += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, timer / fadeOutDuration);
                currentText.color = new Color(color.r, color.g, color.b, alpha);
                //nameText.color = new Color(nameColor.r, nameColor.g, nameColor.b, alpha);
                yield return null;
            }

            if (sentences.Count <= 0)
            {
                DialogueOver();
                
            }
            else
            {
                DisplaySentence();
            }
        }
        else
        {
            yield return new WaitForSeconds(typingRemovalTime);
            DisplaySentence();
        }
    }

    private void InputToNextSentence() // ��ǲ�� �־�� ���� �ؽ�Ʈ�� �Ѿ
    {
        if(Input.anyKeyDown)
        {
            isWaitingForInput = false;

            if (curEffect == DialogueEffect.FadeIn)
            {
                StartCoroutine(AutoNextSentence());
            }
            else
            {
                if(sentences.Count <= 0)
                {
                    DialogueOver();
                    
                }
                else
                {
                    DisplaySentence();
                }
                
            }
        }
    }

    private void OnSkipbuttonClicked() //��ŵ��ư ������
    {
        StopAllCoroutines();
        DialogueOver();
    }

    private void DialogueOver() //���̾�α� ������
    {
        sentences.Clear();
        centerPanel.SetActive(false);
        bottomPanel.SetActive(false);
        skipButton.SetActive(false);
        isWaitingForInput = false;

        dialogueOver?.Invoke();
    }
}
