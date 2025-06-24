using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyModel : MonoBehaviour
{
    public int currentMoney = 100;

    public void SpendMoney(int itemPrice)
    {
        currentMoney -= itemPrice;
    }
}
