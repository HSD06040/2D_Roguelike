using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [SerializeField] private Transform heartBox; //UI판넬

    private List<Image> hearts = new List<Image>();

    public void InicialHearts(int maxHp) //게임시작시 하트 개수 설정
    {
        foreach (var heart in hearts)  //하트 다 지움
        {
            Destroy(heart.gameObject);
        }
        hearts.Clear();

        for (int i = 0; i < maxHp; i++)  //맥스Hp 만큼 새로 추가
        {
            AddHeart();
        }
    }

    public void HeartUpdate(int currentHP)
    {
        for(int i = 0;i < hearts.Count; i++) //리스트를 돌아서 현제 체력만큼만 하트를 키고 나머지는 끔
        {
            if(i < currentHP)  //최대체력만큼의 하트에서 현재체력만큼의 하트는 채움
            {
                hearts[i].sprite = fullHeart; 
            }
            else //나머지 하트는 비움
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }


    private void AddHeart()  //프리팹 복사해서 더함
    {
        GameObject heart = Instantiate(heartPrefab, heartBox);
        Image heartImage = heart.GetComponent<Image>();

        hearts.Add(heartImage);
    }
       

       
}

