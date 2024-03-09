using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpeedBoost Object", menuName = "Inventory System/Item/SpeedBoost")]
public class SpeedBoostObjects : PowerupItemObject
{
    // Start is called before the first frame update
    public float Speedmultiplier;
    void Awake()
    {
        type = ItemType.SpeedBooster;
    }


}
