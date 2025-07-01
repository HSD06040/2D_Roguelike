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
    public string dialogue; //대사 내용
    public DialoguePosition position; // 출력할 위치
    public string speaker; //말하는 인물 이름
    public DialogueEffect effect; // Fade In 이나 Typing
    public DialogueAdvanceType advanceType; // 인풋으로 넘길지 자동으로 넘길지

    public DialogueData(string speaker, string line, DialoguePosition position, DialogueEffect effect, DialogueAdvanceType advanceType) //생성자, 다이얼로그 출력중 변경될 데이터들
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
    [Header("UI 패널")]
    [SerializeField] private GameObject centerPanel;
    [SerializeField] private GameObject bottomPanel;

    [Header("이름 텍스트")]
    [SerializeField] private TMP_Text nameText;

    [Header("대사 텍스트")]
    [SerializeField] private TMP_Text centerDialogueText;
    [SerializeField] private TMP_Text bottomDialogueText;

    [Header("효과 설정")]
    [SerializeField] private float fadeInDuration = 0.5f;
    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private float fadeOutDuration = 2f;
    [SerializeField] private float typingRemovalTime = 2f; 

    [Header("스킵버튼")]
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
    /// 대사를 출력하고 대사의 위치, 효과, 진행방법, 스킵여부를 설정합니다.
    /// </summary>
    /// <param name="lines">문자 목록, 이름, 문자, 포지션 포함</param>
    /// <param name="effect">문자 출력 효과(Fade In, Typing)</param>
    /// <param name="advanceType">대사 넘김 방식(Auto, Manual)</param>
    /// <param name="canSkip">전체 스킵 가능 여부 (true, false)</param>
    public void ShowDialogue(DialogueData[] lines, bool canSkip)
    {
        sentences.Clear(); //큐 비움

        foreach(DialogueData line in lines)
        {
            sentences.Enqueue(line);
        }

        isSkipable = canSkip; //스킵가능여부
        skipButton.SetActive(isSkipable);  //스킵 가능이면 스킵버튼 활성화

        DisplaySentence(); //대사 출력
    }

    public void DisplaySentence()
    {
        if(sentences.Count <= 0)  //출력할게 더이상 없으면
        {
            dialogueOver?.Invoke();
            return;
        }

        if (effectCoroutine != null)  //코루틴이 있으면 없애줌
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

        if(curEffect == DialogueEffect.FadeIn)  //이팩트가 페이드인이라면
        {
            effectCoroutine = StartCoroutine(TextFadeIn(line.dialogue, line.speaker, line.advanceType));
        }

        else if(curEffect == DialogueEffect.Typing) //이팩트가 타이핑이라면
        {
            effectCoroutine = StartCoroutine(TextTyping(line.dialogue, line.speaker, line.advanceType));
        }
    }

    private IEnumerator TextFadeIn(string line, string speaker, DialogueAdvanceType advanceType)  //페이드인 효과 코루틴
    {
        nameText.text = speaker;
        //Color nameColor = nameText.color;
        //nameColor.a = 0;
        //nameText.color = nameColor;

        currentText.text = line;
        Color color = currentText.color;  // 현재텍스트의 컬러를 담아줌
        color.a = 0; //투명도는 0으로 설정
        currentText.color = color; //적용

        float timer = 0f;
        while (timer < fadeInDuration)  //타이머가 듀레이션타임이 되는동안
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeInDuration); //알파라는 변수를 선언하고 0에서 1까지 올려줌
            //nameText.color = new Color(nameColor.r, nameColor.g, nameColor.b, alpha);
            currentText.color = new Color(color.r, color.g, color.b, alpha); //변수 알파를 색상 알파에 넣음
            yield return null;
        }
        color.a = 1f;  // 와일문이 끝나면 알파를 1로 확실히 지정해줌
        //nameColor.a = 1f;

        //nameText.color = nameColor;
        currentText.color = color;  //적용

        if (advanceType == DialogueAdvanceType.Auto)
        {
            StartCoroutine(AutoNextSentence());
        }
        else
        {
            isWaitingForInput = true;
        }

    }

    private IEnumerator TextTyping(string line, string speaker, DialogueAdvanceType advanceType) //타이핑 효과
    {
        nameText.text = speaker;

        currentText.text = ""; //한글자씩 나와야 하니까 디큐한 라인을 초기화해줌

        string displayText = "";
        string curColor = "white"; //기본색상

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
        if (curAdvanceType == DialogueAdvanceType.Auto)  //대사 진행 타입이 오토라면
        {
            StartCoroutine(AutoNextSentence());
        }
        else if (curAdvanceType == DialogueAdvanceType.Manual) //진행 타입이 메뉴얼이면
        {
            isWaitingForInput = true;
        }
    }

    private IEnumerator AutoNextSentence()  //자동으로 다음텍스트로 넘어감
    {
        if (curEffect == DialogueEffect.FadeIn)
        {
            //Color nameColor = nameText.color;
            Color color = currentText.color;
            float timer = 0f;
            while (timer < fadeOutDuration)  //페이드아웃으로 사라짐
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

    private void InputToNextSentence() // 인풋이 있어야 다음 텍스트로 넘어감
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

    private void OnSkipbuttonClicked() //스킵버튼 눌릴때
    {
        StopAllCoroutines();
        DialogueOver();
    }

    private void DialogueOver() //다이얼로그 끝날때
    {
        sentences.Clear();
        centerPanel.SetActive(false);
        bottomPanel.SetActive(false);
        skipButton.SetActive(false);
        isWaitingForInput = false;

        dialogueOver?.Invoke();
    }
}
