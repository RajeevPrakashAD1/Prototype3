using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Bullet Object", menuName = "Inventory System/Item/Bullet")]
public class BulletItemObject : ScriptableObject
{
    public int Id;
    public GameObject model;
    public BulletData bulletdata;
    public string description;
}


