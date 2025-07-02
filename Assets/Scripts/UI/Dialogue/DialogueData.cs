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