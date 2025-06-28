using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPresenter
{
    private GoldPanel goldPanel;

    public GoldPresenter(GoldPanel _goldPanel)
    {
        goldPanel = _goldPanel; 
    }

    public void AddEvent()
    {
        Manager.Data.Gold.AddEvent(goldPanel.UpdateGold);
    }

    public void RemoveEvent()
    {
        Manager.Data.Gold.RemoveEvent(goldPanel.UpdateGold);
    }
}
