using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inven
{
    public Sprite invenImage;
    public string invenName;
    public string invenRepair;
    public string Unitlevel;
    public Inven(Inven inven)
    {
        this.invenImage = inven.invenImage;
        this.invenName = inven.invenName;
        this.invenRepair = inven.invenRepair;
        this.Unitlevel = inven.Unitlevel;
    }
}
