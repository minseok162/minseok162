using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class aaa 
{
    public string UnitName;
    public aaa(aaa unit)
    {
        this.UnitName = unit.UnitName;
    }
}
