using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeart : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartBox; //UI판넬

    private List<GameObject> hearts = new List<GameObject>();

    public void InicialHearts(int startHP) //게임시작시 하트 개수 설정
    {
        for (int i = 0; i < startHP; i++)  //플레이어에서는 startHP = maxHP
        {
            AddHeart();
        }
    }

    public void HeartUpdate(int currentHP)
    {
        while (hearts.Count < currentHP) //하트의 수가 현재 체력보다 작을때 하트 추가
        {
            AddHeart();
        }

        for(int i = 0;i < hearts.Count; i++) //리스트를 돌아서 현제 체력만큼만 하트를 키고 나머지는 끔
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
        GameObject heart = Instantiate(heartPrefab, heartBox); //하트프리팹 복제해서 heartBox에 넣음
        hearts.Add(heart);
    }
       

       
}

