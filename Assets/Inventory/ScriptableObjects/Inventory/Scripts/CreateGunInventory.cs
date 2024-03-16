using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun Inventory", menuName = "Inventory System/Gun Inventory")]
public class GunInventory : InventoryObject<InventoryGunSlot>
{
    // Add any specific methods or properties for gun inventory here
    public void AddItem(Item _item, int _amount, GameObject obj)
    {
      
        if(GameManager.Instance.ActiveSlot == 1) 
        {
            Items[0].UpdateSlot(_item.Id, _item, _amount,obj);
        }
        else
        {
            Items[1].UpdateSlot(_item.Id, _item, _amount,obj);
        }

    }

    public void RemoveItem(int id)
    {
        Debug.Log("removing slot id of" + id);
        if (id == 1) Items[0].UpdateSlot(-1, null, 0);
        if (id == 2) Items[1].UpdateSlot(-1, null, 0);


    }



}
