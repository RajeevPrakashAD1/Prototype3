﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName ="New Inventory",menuName ="Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    // Start is called before the first frame update
    public ItemDatabaseObject database;
    //public List<InventorySlot> Container = new List<InventorySlot>();
    public Inventory Container;

    public string savePath;
    public void AddItem(Item _item,int _amount)
    {

        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].ID == _item.Id)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        SetEmptySlot(_item, _amount);

    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for(int i = 0; i < Container.Items.Length; i++)
        {
            if(Container.Items[i].ID <= 1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Items[i];
            }
        }

        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount);
    }


    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < Container.Items.Length; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Container.Items[i].UpdateSlot(-1, null, 0);
            }
        }
    }




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
}






[System.Serializable]
public class Inventory
{
    public InventorySlot[] Items = new InventorySlot[9];
    public Inventory()
    {
        Items = new InventorySlot[9];
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i] = new InventorySlot();
        }
    }
}
















[System.Serializable]
public class InventorySlot
{
    public int ID;
    public Item item;
    public int amount;
 
    
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id,Item _item,int _amount)
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