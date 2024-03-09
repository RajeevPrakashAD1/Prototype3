using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Item Database",menuName ="Inventory System/Item/Database")]
public class ItemDatabaseObject : ScriptableObject
{
    // Start is called before the first frame update
    public PowerupItemObject[] PowerUpItems;
    public GunItemObject[] GunItems;
    //public InventoryObject inventory;
    
    public Dictionary<int,PowerupItemObject> GetItemPowerUp = new Dictionary<int,PowerupItemObject>();
    public Dictionary<int, GunItemObject> GetItemGun = new Dictionary<int, GunItemObject>();

    public void OnEnable()
    {
        //Debug.Log("database called "+PowerUpItems.Length);
      
      
        for(int i = 0; i < PowerUpItems.Length; i++)
        {
            //Debug.Log("item name", Item[i].Name);
            //Items[i].Id = i;
            //Items[i].name = "something";
            //Debug.Log(Items[i].Id + "  " + Items[i].type);
            GetItemPowerUp.Add(PowerUpItems[i].Id, PowerUpItems[i]);
            //inventory.AddItem(new Item(Items[i]),1);
            
        }
        for(int i = 0; i < GunItems.Length; i++)
        {
            GetItemGun.Add(GunItems[i].Id, GunItems[i]);
        }
    }

 /*   public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, ItemObject>();
    }*/
}
