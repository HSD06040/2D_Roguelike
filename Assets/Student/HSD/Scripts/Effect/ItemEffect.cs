using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
    public abstract void Execute(string _source, int _upgrade);
    public abstract void Revoke(string _source, int _upgrade);
}
