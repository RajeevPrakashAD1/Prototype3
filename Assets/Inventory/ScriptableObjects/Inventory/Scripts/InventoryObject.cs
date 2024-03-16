﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;






public abstract class InventoryObject<T> : ScriptableObject where T : InventorySlot
{
    public T[] Items;

    public void AddItem(Item _item, int _amount)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].ID == _item.Id)
            {
                Items[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);
    }
    

    public T SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].ID < 1)
            {
                Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Items[i];
            }
        }
        Items[Items.Length - 1].UpdateSlot(_item.Id, _item, _amount);
        return Items[Items.Length - 1];
    }
   

    public bool MoveItem(T item1, T item2)
    {
        if (item1.GetType() == item2.GetType())
        {
            T temp = (T)System.Activator.CreateInstance(typeof(T), item2.ID, item2.item, item2.amount);
            item2.UpdateSlot(item1.ID, item1.item, item1.amount);
            item1.UpdateSlot(temp.ID, temp.item, temp.amount);
            return true;
        }
        else
        {
            Debug.LogError("Cannot move items of different types.");
        }
        return false;
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i].item == _item)
            {
                Items[i].UpdateSlot(-1, null, 0);
            }
        }
    }
    
}



















/*
[System.Serializable]
public class Inventory
{
    public InventoryPowerUpSlot[] PowerUpItems = new InventoryPowerUpSlot[6];
    public InventoryGunSlot[] GunItems = new InventoryGunSlot[2]; // Array for gun slots
    public InventoryBulletSlot[] BulletItems = new InventoryBulletSlot[4]; // Array for bullet slots

    
}*/












//inventort slot................






[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;
    public int amount; // Moved from derived classes

    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0; // Default amount
    }

    public InventorySlot(int _id, Item _item, int _amount) // Added _amount parameter
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void UpdateSlot(int _id, Item _item, int _amount) // Added _amount parameter
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    

    public void AddAmount(int value)
    {
        amount += value;
    }
}



[System.Serializable]
public class InventoryPowerUpSlot : InventorySlot
{
   

    public InventoryPowerUpSlot(int _id, Item _item, int _amount) : base(_id, _item,_amount)
    {
        
    }

    
}

[System.Serializable]
public class InventoryGunSlot : InventorySlot
{
    public GameObject gun;
    public InventoryGunSlot() : base()
    {

    }
    public InventoryGunSlot(int _id, Item _item,int _amount) : base(_id, _item,_amount)
    {
    }
    public void UpdateSlot(int _id, Item _item, int _amount,GameObject obj) // Added _amount parameter
    {
        ID = _id;
        item = _item;
        amount = _amount;
        gun = obj;
    }


}


[System.Serializable]
public class InventoryBulletSlot : InventorySlot
{
    

    public InventoryBulletSlot(int _id, Item _item, int _amount) : base(_id, _item,_amount)
    {
       
    }

    
}





[System.Serializable]
public class Item
{
    public string Name;
    public int Id;
    public Item(PowerupItemObject item)
    {
        if (item.name != null) Name = item.name;
        Id = item.Id;
    }
    public Item(GunItemObject item)
    {
        Name = "Gun";
        Id = item.Id;
    }
    public Item(BulletItemObject item)
    {
        Name = "Bullet";
        Id = item.Id;
    }

    public Item(int i)
    {
        Name = "";
        Id = i;
    }
    public Item()
    {

    }
}












































/*[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabaseObject database;
    public Inventory Container;
    public string savePath;

    public void AddItem(Item _item, int _amount)
    {
        for (int i = 0; i < Container.PowerUpItems.Length; i++)
        {
            if (Container.PowerUpItems[i].ID == _item.Id)
            {
                Container.PowerUpItems[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);
    }

    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < Container.PowerUpItems.Length; i++)
        {
            if (Container.PowerUpItems[i].ID <= 1)
            {
                Container.PowerUpItems[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.PowerUpItems[i];
            }
        }
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        if (item1.GetType() == item2.GetType())
        {
            InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
            item2.UpdateSlot(item1.ID, item1.item, item1.amount);
            item1.UpdateSlot(temp.ID, temp.item, temp.amount);
        }
        else
        {
            Debug.LogError("Cannot move items of different types.");
        }
    }

    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.PowerUpItems.Length; i++)
        {
            if (Container.PowerUpItems[i].item == _item)
            {
                Container.PowerUpItems[i].UpdateSlot(-1, null, 0);
            }
        }
    }
}*/













/*
[System.Serializable]
public class InventoryPowerUpSlot
{
    public int ID;
    public Item item;
    public int amount;
 
    
    public InventoryPowerUpSlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventoryPowerUpSlot(int _id,Item _item,int _amount)
    {
        item = _item;
        amount = _amount;
        ID = _id;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void  UpdateSlot(int _id, Item _item, int _amount)
    {


        item = _item;
        amount = _amount;
        ID = _id;
    }
}

[System.Serializable]
public class InventoryGunSlot
{
    public int ID;
    public Item item;

    public InventoryGunSlot()
    {
        ID = -1;
        item = null;
    }

    public InventoryGunSlot(int _id, Item _item)
    {
        item = _item;
        ID = _id;
    }

    public void UpdateSlot(int _id, Item _item)
    {
        item = _item;
        ID = _id;
    }
}

[System.Serializable]
public class InventoryBulletSlot
{
    public int ID;
    public Item item;
    public int amount;

    public InventoryBulletSlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }

    public InventoryBulletSlot(int _id, Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
        ID = _id;
    }

    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        item = _item;
        amount = _amount;
        ID = _id;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
*/
































//save and delete of inventory object

/*[ContextMenu("Save")]
 public void Save()
 {
     //string saveData = JsonUtility.ToJson(this, true);
     //BinaryFormatter bf = new BinaryFormatter();
     //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
     //bf.Serialize(file, saveData);
     //file.Close();

     IFormatter formatter = new BinaryFormatter();
     Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
     formatter.Serialize(stream, Container);
     stream.Close();
 }



 [ContextMenu("Load")]
 public void Load()
 {
     if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
     {
         //BinaryFormatter bf = new BinaryFormatter();
         //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
         //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
         //file.Close();

         IFormatter formatter = new BinaryFormatter();
         Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
         Inventory newContainer = (Inventory)formatter.Deserialize(stream);
         for (int i = 0; i < Container.Items.Count; i++)
         {
             Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount);
         }
         stream.Close();
     }
 }


 [ContextMenu("Clear")]
 public void Clear()
 {
     Container = new Inventory();
 }*/