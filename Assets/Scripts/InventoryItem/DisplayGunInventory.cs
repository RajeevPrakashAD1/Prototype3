using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class DisplayGunInventory : MonoBehaviour
{
    public MouseItem mouseItem = new MouseItem();
    public GameObject inventoryPowerUpPrefab;

    public GameObject tickButtonPrefab;
    private GameObject currentTickButton;
    public GameObject Player;
    public GunInventory inventory;
    public ItemDatabaseObject database;
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEMS;
    public EquipPowerUp equip;
    Dictionary<GameObject, InventoryGunSlot> GunItemsDisplayed = new Dictionary<GameObject, InventoryGunSlot>();
    Dictionary<GameObject, int> SlotId = new Dictionary<GameObject, int>();
    private bool gunEquipped;
    public GameObject newGunEquipped;
    public GameObject gunSlot1;
    public GameObject gunSlot2;
    public PickGun pickGun;
    void Start()
    {
        pickGun = Player.GetComponent<PickGun>();
        inventory.Items = new InventoryGunSlot[2];
        CreateSlots();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    public void CreateSlots()
    {
        GunItemsDisplayed = new Dictionary<GameObject, InventoryGunSlot>();
        SlotId.Clear();
    
        
        for(int i = 0; i < inventory.Items.Length; i++) { 
            var obj = Instantiate(inventoryPowerUpPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).gameObject.SetActive(false);
            obj.GetComponent<RectTransform>().localPosition = GetPowerUpSlotPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });


            inventory.Items[i] = new InventoryGunSlot();
            GunItemsDisplayed.Add(obj, inventory.Items[i]);
            SlotId[obj] = i+1;
            // Debug.Log(inventory.Container.Items[i].item.Name);
        }
        

    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventoryGunSlot> _slot in GunItemsDisplayed)
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
                    //Debug.Log("chil not nll");
                }
                //Debug.Log("slot................ " + _slot.Key.transform.GetChild(0));

                _slot.Key.transform.GetComponent<Image>().sprite = database.GetItemGun[_slot.Value.item.Id].gundata.img;
                _slot.Key.transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                //_slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");

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
        //Debug.Log("position " + pos);
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)) - 800, Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    //jo@rentomojo.com

    
    public void OnEnter(GameObject obj)
    {
        // Debug.Log("mouse Item hovering.... on enter");
        mouseItem.hoverObj = obj;
        if (GunItemsDisplayed.ContainsKey(obj))
            mouseItem.hoverItem = GunItemsDisplayed[obj];
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


        if (GunItemsDisplayed[obj].ID >= 0)
        {

            var img = mouseObject.AddComponent<Image>();
            // Debug.Log("get insied"+ itemsDisplayed[obj].ID +"...."+ inventory.database.GetItem[itemsDisplayed[obj].ID].img);
            img.sprite = database.GetItemGun[GunItemsDisplayed[obj].ID].gundata.img;
            img.raycastTarget = false;
        }
        mouseItem.obj = mouseObject;
        mouseItem.item = GunItemsDisplayed[obj];         
    }
    public void OnDragEnd(GameObject obj)
    {
        if (mouseItem.hoverObj)
        {
            bool exchanged =  inventory.MoveItem(GunItemsDisplayed[obj], GunItemsDisplayed[mouseItem.hoverObj]);
            if (exchanged)
            {
                Debug.Log("exchanged...");
                if(GameManager.Instance.ActiveSlot == 1)
                {
                    gunSlot2.SetActive(true);
                }
                if(GameManager.Instance.ActiveSlot == 2)
                {
                    gunSlot1.SetActive(true);
                }
                Transform temp1 = null;
                Transform temp2 = null;
                if (gunSlot1.transform.childCount > 0)
                {

                     temp1 = gunSlot1.transform.GetChild(0);
                    
                }

                if (gunSlot2.transform.childCount > 0)
                {
                     temp2 = gunSlot2.transform.GetChild(0);
                   
                }
                if (temp1) temp1.SetParent(gunSlot2.transform);
                if (temp2) temp2.SetParent(gunSlot1.transform);

                
                if (GameManager.Instance.ActiveSlot == 1)
                {
                    gunSlot2.SetActive(false);
                }
                if (GameManager.Instance.ActiveSlot == 2)
                {
                    gunSlot1.SetActive(false);
                }


            }


        }
        else
        {
            Debug.Log("aya isme");
            inventory.RemoveItem(SlotId[obj]);
            pickGun.Drop(SlotId[obj]);
            //DestroyTickButton();
            Destroy(newGunEquipped);
           /* GunMain gunMain = newGunEquipped.GetComponent<GunMain>();
            MeshRenderer mr = newGunEquipped.GetComponent<MeshRenderer>();
            gunMain.enabled = false;
            mr.enabled = false;*/


        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
        pickGun.Pick();
    }
    public void OnDrag(GameObject obj)
    {
        if (mouseItem.obj != null)
            mouseItem.obj.GetComponent<RectTransform>().position = Input.mousePosition;
    }



    private void ShowTickButton(Vector3 position)
    {
        // Destroy the current tick button if it exists
        DestroyTickButton();

        // Instantiate the tick button prefab
        currentTickButton = Instantiate(tickButtonPrefab, position + new Vector3(10f,0f,10f), Quaternion.identity, transform);
    }

    private void DestroyTickButton()
    {
        // Destroy the current tick button if it exists
        if (currentTickButton != null)
        {
            Destroy(currentTickButton);
            currentTickButton = null;
        }
    }


}






/*
public class MouseItem
{
    public GameObject obj;
    public InventoryPowerUpSlot item;
    public InventoryPowerUpSlot hoverItem;
    public GameObject hoverObj;
}*/

