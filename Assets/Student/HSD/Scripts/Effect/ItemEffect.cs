using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
    public abstract void Check(string _source, int _upgrade);
    public abstract void Active(string _source, int _upgrade);
    public abstract void DeActive(string _source, int _upgrade);
}
