using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Item")]
    public int ID;
    public int size;
    public string itemName;
    public GameObject model;
    [TextArea] 
    public string description;
    public Sprite icon;
    public int Price;
}
