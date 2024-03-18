using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBulletInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public ItemDatabaseObject database;
    public Dictionary<GameObject, InventorySlot> PowerUpitemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddBulletToInventorySlot(GameObject obj,InventorySlot slot)
    {
        PowerUpitemsDisplayed.Add(obj, slot);
    }

    public void HandleSlotClick(GameObject obj)
    {
        if (!PowerUpitemsDisplayed.ContainsKey(obj) || PowerUpitemsDisplayed[obj].ID < 0)
        {
            return;
        }
        // bulletManager.SetBullet(database.GetItemBullet[PowerUpitemsDisplayed[obj].ID].model, PowerUpitemsDisplayed[obj].ID);

        //gunmain.chooseBullet();
        
    }

    public (GameObject, int) ChooseBullet(int i)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in PowerUpitemsDisplayed)
        {
            /*  Debug.Log(_slot.Value);
              Debug.Log(_slot.Key);
              Debug.Log(_slot.Value + "   " + _slot.Value.ID + " ");*/

            if (_slot.Value.ID >= 0)

            {
                //Debug.Log("i have something");
                int type = database.GetItemBullet[_slot.Value.item.Id].bulletdata.type;
                if (i == type) return (database.GetItemBullet[_slot.Value.item.Id].model, _slot.Value.item.Id);
            }

        }
        return (null, -1);
    }
}
