using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Item/Food")]
public class FoodObjects : ItemObjects
{
    // Start is called before the first frame update
    public float energy;
    void Awake()
    {
        type = ItemType.Food;
    }


}
