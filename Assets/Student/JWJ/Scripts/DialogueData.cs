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