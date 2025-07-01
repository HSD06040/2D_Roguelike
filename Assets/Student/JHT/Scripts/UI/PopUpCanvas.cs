using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpCanvas : MonoBehaviour
{
    private Stack<BaseUI> popUpStack = new Stack<BaseUI>();

    public void AddUI(BaseUI ui)
    {
        if(popUpStack.Count > 0)
        {
            BaseUI top = popUpStack.Peek();
            top.gameObject.SetActive(false);
        }

        popUpStack.Push(ui);
    }

    public void RemoveUI()
    {
        if (popUpStack.Count == 0)
            return;

        BaseUI top = popUpStack.Pop();
        Destroy(top.gameObject);

        if(popUpStack.Count >0)
        {
            top = popUpStack.Peek();
            top.gameObject.SetActive(true);
        }
    }
}
