using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Default,
    Food,
    SpeedBooster,
    Protection
}
public abstract class PowerupItemObject : ScriptableObject
{
    public int Id;
    public Sprite img;
    public ItemType type;
    public string description;
}

