using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Item Database",menuName ="Inventory System/Item/Database")]
public class ItemDatabaseObject : ScriptableObject
{
    // Start is called before the first frame update
    public ItemObject[] Items;
    //public InventoryObject inventory;
    
    public Dictionary<int,ItemObject> GetItem = new Dictionary<int,ItemObject>();

    public void OnEnable()
    {
        Debug.Log("database called "+Items.Length);
      
      
        for(int i = 0; i < Items.Length; i++)
        {
            //Debug.Log("item name", Item[i].Name);
            //Items[i].Id = i;
            //Items[i].name = "something";
            //Debug.Log(Items[i].Id + "  " + Items[i].type);
            GetItem.Add(Items[i].Id, Items[i]);
            //inventory.AddItem(new Item(Items[i]),1);
            
        }
    }

 /*   public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemObject>();
    }*/
}
