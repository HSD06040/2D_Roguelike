using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeart : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartBox; //UI�ǳ�

    private List<GameObject> hearts = new List<GameObject>();

    public void InicialHearts(int startHP) //���ӽ��۽� ��Ʈ ���� ����
    {
        for (int i = 0; i < startHP; i++)  //�÷��̾���� startHP = maxHP
        {
            AddHeart();
        }
    }

    public void HeartUpdate(int currentHP)
    {
        while (hearts.Count < currentHP) //��Ʈ�� ���� ���� ü�º��� ������ ��Ʈ �߰�
        {
            AddHeart();
        }

        for(int i = 0;i < hearts.Count; i++) //����Ʈ�� ���Ƽ� ���� ü�¸�ŭ�� ��Ʈ�� Ű�� �������� ��
        {
            if(i < currentHP)
            {
                hearts[i].SetActive(true);
            }
            else 
            {
                hearts[i].SetActive(false);
            }
        }
    }


    private void AddHeart()
    {
        GameObject heart = Instantiate(heartPrefab, heartBox); //��Ʈ������ �����ؼ� heartBox�� ����
        hearts.Add(heart);
    }
       

       
}

