using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Gun Object", menuName = "Inventory System/Item/Gun")]
public class GunItemObject : ScriptableObject
{
    public int Id;
    public GameObject model;
    public GunData gundata;
    public string description;
}


