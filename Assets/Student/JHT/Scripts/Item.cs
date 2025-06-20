using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Item : ScriptableObject
{
    public int ID;
    public int size;
    public string itmeName;
    public GameObject model;
    [TextArea] public string description = null;
    public Sprite icon = null;
    
}
