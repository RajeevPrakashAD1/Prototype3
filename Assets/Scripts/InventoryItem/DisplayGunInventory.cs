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
    Dictionary<GameObject, InventorySlot> GunItemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    private bool gunEquipped;
    public GameObject newGunEquipped;

    void Start()
    {
        inventory.Items = new InventorySlot[4];
        CreateSlots();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSlots();
    }

    public void CreateSlots()
    {
        GunItemsDisplayed = new Dictionary<GameObject, InventorySlot>();
       Debug.Log("gun inventory length++" + inventory.Items.Length);
        for (int i = 0; i < inventory.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPowerUpPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPowerUpSlotPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });

            inventory.Items[i] = new InventorySlot();
            GunItemsDisplayed.Add(obj, inventory.Items[i]);
            // Debug.Log(inventory.Container.Items[i].item.Name);

        }

    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in GunItemsDisplayed)
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

    public void OnClick(GameObject obj)
    {
        Debug.Log("onclick");
        if (!GunItemsDisplayed.ContainsKey(obj) || GunItemsDisplayed[obj].ID < 0)
        {
            return;
        }
        void ChangeGun(GameObject newGun)
        {
            // Find the child object with the "Gun" tag
            Transform oldGunTransform = null;
            
            //Debug.Log("player" + Player);
            // if(Player != null) Debug.Log("player not null");
            if (Player && Player.transform && Player.transform.childCount >= 1)
            {
                oldGunTransform = Player.transform.GetChild(0);
            }


            //Debug.Log("old gun" + oldGunTransform.position);
            if (oldGunTransform != null)
            {
                // Save the position and rotation of the old gun
                Vector3 oldGunPosition = oldGunTransform.position;
                Quaternion oldGunRotation = oldGunTransform.rotation;

                // Destroy the old gun
                // Debug.Log("destroying old gun");
                if(Player.transform.childCount > 5)
                {
                    Destroy(Player.transform.GetChild(5).gameObject);
                }

                // Create a new gun object and set its position and rotation
                GameObject instantiatedNewGun = Instantiate(newGun, oldGunPosition, oldGunRotation, Player.transform);
                instantiatedNewGun.tag = "PlayerGun";
                instantiatedNewGun.layer = 12;
                GunMain gunMain = instantiatedNewGun.GetComponent<GunMain>();
               
                


                gunMain.equipped = true;
                newGunEquipped = instantiatedNewGun;

            }
            else
            {
                Debug.Log("No gun object found as a child of the player.");




            }
        }

        GameObject gunn = database.GetItemGun[GunItemsDisplayed[obj].ID].model;
       ChangeGun(gunn);
       ShowTickButton(obj.transform.position);
        gunEquipped = true;
    }
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
            inventory.MoveItem(GunItemsDisplayed[obj], GunItemsDisplayed[mouseItem.hoverObj]);
            if(gunEquipped) ShowTickButton(mouseItem.hoverObj.transform.position);

        }
        else
        {
            Debug.Log("aya isme");
            inventory.RemoveItem(GunItemsDisplayed[obj].item);
            DestroyTickButton();
            Destroy(newGunEquipped);
           /* GunMain gunMain = newGunEquipped.GetComponent<GunMain>();
            MeshRenderer mr = newGunEquipped.GetComponent<MeshRenderer>();
            gunMain.enabled = false;
            mr.enabled = false;*/


        }
        Destroy(mouseItem.obj);
        mouseItem.item = null;
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

