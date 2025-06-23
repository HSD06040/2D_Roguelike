using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Item : ScriptableObject
{
    public int ID;
    public int size;
    public string itemName;
    public GameObject model;
    [TextArea] public string description = null;
    public Sprite icon = null;
    
}
