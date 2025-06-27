using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [SerializeField] private Transform heartBox; //UI�ǳ�

    private List<Image> hearts = new List<Image>();

    public void InicialHearts(int maxHp) //���ӽ��۽� ��Ʈ ���� ����
    {
        foreach (var heart in hearts)  //��Ʈ �� ����
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear();

        for (int i = 0; i < maxHp; i++)  //�ƽ�Hp ��ŭ ���� �߰�
        {
            AddHeart();
        }
    }

    public void HeartUpdate(int currentHP)
    {
        for(int i = 0;i < hearts.Count; i++) //����Ʈ�� ���Ƽ� ���� ü�¸�ŭ�� ��Ʈ�� Ű�� �������� ��
        {
            if(i < currentHP)  //�ִ�ü�¸�ŭ�� ��Ʈ���� ����ü�¸�ŭ�� ��Ʈ�� ä��
            {
                hearts[i].sprite = fullHeart; 
            }
            else //������ ��Ʈ�� ���
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }


    private void AddHeart()  //������ �����ؼ� ����
    {
        GameObject heart = Instantiate(heartPrefab, heartBox);
        Image heartImage = heart.GetComponent<Image>();

        hearts.Add(heartImage);
    }
       

       
}

