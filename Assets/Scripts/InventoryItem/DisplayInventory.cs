using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{


    //public GameObject inventoryPrefab;
    public InventoryObject inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;
    Dictionary<InventorySlot,GameObject> itemsDisplayed = new Dictionary< InventorySlot, GameObject>();
    void Start()
    {
        CreateSlots();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }
    public void CreateSlots()
    {
        //itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Count; i++)
        {

            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            TextMeshProUGUI tmp = obj.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null) { tmp.text = inventory.Container[i].amount.ToString("n0"); }
            else { Debug.Log("Text mesh Pro is null"); }
            itemsDisplayed[inventory.Container[i]] = obj;




            //itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }
    public void UpdateSlots()
    {
       for(int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                TextMeshProUGUI tmp = obj.GetComponentInChildren<TextMeshProUGUI>();
                if (tmp != null) { tmp.text = inventory.Container[i].amount.ToString("n0"); }
                else { Debug.Log("Text mesh Pro is null"); }
                itemsDisplayed[inventory.Container[i]] = obj;

            }
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
}