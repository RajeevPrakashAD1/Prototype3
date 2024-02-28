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
public abstract class ItemObjects : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    public string description;
}
