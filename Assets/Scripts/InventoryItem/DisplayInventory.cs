using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    public GameObject InventoryPanel;
    /*public MouseItem mouseItem = new MouseItem();
    public GameObject inventoryPowerUpPrefab;
    public GameObject inventoryGunPrefab;
    // public GameObject GunSlot;
    
    public PowerUpInventory inventory;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;



    public int X_START_GUN;
    public int Y_START_GUN;
    public int X_SPACE_BETWEEN_ITEM_GUN;
    public int NUMBER_OF_COLUMN_GUN;
    public int Y_SPACE_BETWEEN_ITEMS_GUN;

    public int X_START_BULLET;
    public int Y_START_BULLET;
    public int X_SPACE_BETWEEN_ITEM_BULLET;
    public int NUMBER_OF_COLUMN_BULLET;
    public int Y_SPACE_BETWEEN_ITEMS_BULLET;

    public EquipPowerUp equip;
    Dictionary<GameObject, InventoryPowerUpSlot> PowerUpitemsDisplayed = new Dictionary<GameObject, InventoryPowerUpSlot>();
    Dictionary<GameObject, InventoryGunSlot> GunitemsDisplayed = new Dictionary<GameObject, InventoryGunSlot>();
    //Dictionary<GameObject, InventoryPowerUpSlot> BulletitemsDisplayed = new Dictionary<GameObject, InventoryPowerUpSlot>();
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
        PowerUpitemsDisplayed = new Dictionary<GameObject, InventoryPowerUpSlot>();
        // Debug.Log("inventory length++" + inventory.Container.Items.Length);
        for (int i = 0; i < inventory.Container.PowerUpItems.Length; i++)
        {
            var obj = Instantiate(inventoryPowerUpPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPowerUpSlotPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });


            PowerUpitemsDisplayed.Add(obj, inventory.Container.PowerUpItems[i]);
            // Debug.Log(inventory.Container.Items[i].item.Name);

        }
        for (int i = 0; i < inventory.Container.GunItems.Length; i++)
        {
            var obj = Instantiate(inventoryGunPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetGunSlotPosition(i);
            GunitemsDisplayed.Add(obj, inventory.Container.GunItems[i]);
        }
    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventoryPowerUpSlot> _slot in PowerUpitemsDisplayed)
        {
            //Debug.Log(_slot.Key + "   " + _slot.Value.ID + " "+ _slot.Value.item.Name);

            if (_slot.Value.ID >= 0)

            {

                if (_slot.Value.amount <= 0)
                {
                    _slot.Key.transform.GetComponent<Image>().sprite = null;
                    //_slot.Key.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                    _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                    _slot.Value.UpdateSlot(-1, null, 0);
                    return;
                }

                // Debug.Log("update slot" + _slot.Value.ID);
                if (_slot.Key.transform.GetComponent<Image>() == null)
                {
                    Debug.Log("child is null");
                }
                else
                {
                    Debug.Log("chil not nll");
                }
                Debug.Log("slot................ " + _slot.Key.transform.GetChild(0));

                _slot.Key.transform.GetComponent<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].img;
                _slot.Key.transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");

            }
            else
            {
                _slot.Key.transform.GetComponent<Image>().sprite = null;
                //_slot.Key.transform.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    public Vector3 GetPowerUpSlotPosition(int i)
    {
        float pos = (X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)));
        Debug.Log("position " + pos);
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)) + 400, Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
    public Vector3 GetGunSlotPosition(int i)
    {
        //float pos = (X_START_GUN + (X_SPACE_BETWEEN_ITEM_GUN * (i % NUMBER_OF_COLUMN_GUN)));
        //Debug.Log("position " + pos);
        return new Vector3(X_START_GUN + (X_SPACE_BETWEEN_ITEM_GUN * (i % NUMBER_OF_COLUMN_GUN)) - 400, Y_START_GUN + (-Y_SPACE_BETWEEN_ITEMS_GUN * (i / NUMBER_OF_COLUMN_GUN)) + 600, 0f);
    }


    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }


    public void OnClick(GameObject obj)
    {
        if (PowerUpitemsDisplayed.ContainsKey(obj) && PowerUpitemsDisplayed[obj].ID >= 1)
        {
            // Handle the click event for the item
            ItemObject clickItem = inventory.database.GetItem[PowerUpitemsDisplayed[obj].ID];
            string type = clickItem.type.ToString();
            if (type == "Food")
            {
                FoodObjects food = clickItem as FoodObjects;
                Debug.Log("food energy" + food.energy);
                equip.IncreasePlayerHealth(food.energy);
                PowerUpitemsDisplayed[obj].amount -= 1;
            }
            else if (type == "SpeedBooster")
            {
                SpeedBoostObjects speedObject = clickItem as SpeedBoostObjects;
                equip.IncreasePlayerSpeed(speedObject.Speedmultiplier, 10f);
                PowerUpitemsDisplayed[obj].amount -= 1;
            }
            else if (type == "Protection")
            {
                ProtectionObjects proobj = clickItem as ProtectionObjects;
                equip.StartProtectionSphere(proobj.protectionTime);
                PowerUpitemsDisplayed[obj].amount -= 1;
            }
            Debug.Log("Clicked on item: " + PowerUpitemsDisplayed[obj].item.Name);
        }
    }
    public void OnEnter(GameObject obj)
    {
        // Debug.Log("mouse Item hovering.... on enter");
        mouseItem.hoverObj = obj;
        if (PowerUpitemsDisplayed.ContainsKey(obj))
            mouseItem.hoverItem = PowerUpitemsDisplayed[obj];
    }
    public void OnExit(GameObject obj)
    {
        mouseItem.hoverObj = null;
        mouseItem.hoverItem = null;
    }
    public void OnDragStart(GameObject obj)
    {
        var mouseObject = new GameObject();
        var rt = mouseObject.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(50, 50);
        mouseObject.transform.SetParent(transform.parent);


        if (PowerUpitemsDisplayed[obj].ID >= 0)
        {

            var img = mouseObject.AddComponent<Image>();
            // Debug.Log("get insied"+ itemsDisplayed[obj].ID +"...."+ inventory.database.GetItem[itemsDisplayed[obj].ID].img);
            img.sprite = inventory.database.GetItem[PowerUpitemsDisplayed[obj].ID].img;
            img.raycastTarget = false;
        }
        mouseItem.obj = mouseObject;
        mouseItem.item = PowerUpitemsDisplayed[obj];
    }
    public void OnDragEnd(GameObject obj)
    {
        if (mouseItem.hoverObj)
        {
            inventory.MovePowerUpItem(PowerUpitemsDisplayed[obj], PowerUpitemsDisplayed[mouseItem.hoverObj]);
        }
        else
        {
            inventory.RemovePowerUpItem(PowerUpitemsDisplayed[obj].item);
        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
    }
    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
    }
*/

    public void Toggle()
    {

        if (InventoryPanel.activeInHierarchy == false)
        {
            InventoryPanel.SetActive(true);
        }
        else
        {
            InventoryPanel.SetActive(false);
        }
    }



}



public class MouseItem
{
    public GameObject obj;
    public InventorySlot item;
    public InventorySlot hoverItem;
    public GameObject hoverObj;
}