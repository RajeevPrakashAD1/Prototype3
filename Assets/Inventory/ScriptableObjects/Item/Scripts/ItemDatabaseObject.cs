using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Item Database",menuName ="Inventory System/Item/Database")]
public class ItemDatabaseObject : ScriptableObject,ISerializationCallbackReceiver
{
    // Start is called before the first frame update
    public ItemObjects[] Items;
   
    public Dictionary<ItemObjects, int> GetId = new Dictionary<ItemObjects, int>();
  
    public Dictionary<int,ItemObjects> GetItem = new Dictionary<int,ItemObjects>();

    public void OnAfterDeserialize()
    {
        Debug.Log("database called");
        GetId = new Dictionary<ItemObjects, int>();
        GetItem = new Dictionary<int,ItemObjects>();
        for(int i = 0; i < Items.Length; i++)
        {
            GetId.Add(Items[i], i);
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        //throw new System.NotImplementedException();
    }
}
