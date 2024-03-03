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
public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite img;
    public ItemType type;
    public string description;
}

[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public Item(ItemObject item)
    {
        if(item.name != null) Name = item.name;
        Id = item.Id;
    }
    public Item(int i)
    {
        Name = "ch1" + i.ToString();
        Id = -1;
    }
    public Item()
    {

    }
}