using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food Object", menuName = "Inventory System/Item/Food")]
public class FoodObjects : PowerupItemObject
{
    // Start is called before the first frame update
    public int energy;
    void Awake()
    {
        type = ItemType.Food;
    }


}
