using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Protection Object", menuName = "Inventory System/Item/Protection")]
public class ProtectionObjects : ItemObject
{
    // Start is called before the first frame update
    public float protectionTime;
    void Awake()
    {
        type = ItemType.Protection;
    }


}
