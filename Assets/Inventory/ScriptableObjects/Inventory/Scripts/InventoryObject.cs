﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[CreateAssetMenu(fileName ="New Inventory",menuName ="Inventory System/Inventory")]
public class InventoryObject : ScriptableObject,ISerializationCallbackReceiver
{
    // Start is called before the first frame update
    public ItemDatabaseObject database;
    public List<InventorySlot> Container = new List<InventorySlot>();
    public string savePath;
    public void AddItem(ItemObjects _item,int _amount)
    {
       
        for(int i=0;i < Container.Count; i++)
        {
            if(Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }

      
        
        Container.Add(new InventorySlot(database.GetId[_item],_item, _amount));
        
    }

    public void OnAfterDeserialize()
    {
        for(int i = 0; i < Container.Count; i++)
        {
            //Debug.Log("container" + Container);
            if(database.GetItem.ContainsKey(Container[i].Id)   )Container[i].item = database.GetItem[Container[i].Id];
        }
    }

    public void OnBeforeSerialize()
    {
       
    }

    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
    }
    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
        JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
        file.Close();
    }
}

[System.Serializable]
public class InventorySlot
{
    public int Id;
    public ItemObjects item;
    public int amount;
    public InventorySlot(int _id,ItemObjects _item,int _amount)
    {
        item = _item;
        amount = _amount;
        Id = _id;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}