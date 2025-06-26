using UnityEngine;

public class MonsterTracker : MonoBehaviour //���� Ȯ���� ���� ȹ�� ���
{
    public GameObject[] monsters;         // ������ ���͵�
    public GameObject chestToActivate;    // ���� ������Ʈ (�ʱ⿣ ��Ȱ��ȭ�Ǿ� ����)

    void Start()
    {
        chestToActivate.SetActive(false); // ���� �� ���� ����
    }

    void Update()
    {
        // ��� ���Ͱ� �׾����� Ȯ��
        bool allDead = true;
        foreach (GameObject monster in monsters)
        {
            if (monster != null) // ��� �ִ� ���Ͱ� �ִٸ�
            {
                allDead = false;
                break;
            }
        }

        // ��� �׾����� ���� Ȱ��ȭ
        if (allDead && !chestToActivate.activeSelf)
        {
            Debug.Log("��� ���͸� óġ�߽��ϴ�. ���ڰ� ��Ÿ���ϴ�!");
            chestToActivate.SetActive(true);
        }
    }
}
