using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class GunSlots : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gunslot1;
    public GameObject gunslot2;
    public ItemDatabaseObject database;
    public GunInventory gunInventory;
    public GameObject Player;
    void Start()
    {
        
        AddEvent(gunslot1, EventTriggerType.PointerClick, delegate { OnClick(1); });
        AddEvent(gunslot2, EventTriggerType.PointerClick, delegate { OnClick(2); });
       
    }

    // Update is called once per frame
    void Update()
    {
        //scope of improvement calling in update is not cool u should be calling this function where it is getting updated.
        UpdateSlot();
    }
    public void UpdateSlot()
    {
        //updating guninventory(displayed on screen two gun slots) slot on the basis that inventory slot get some gun
        if(gunInventory.Items[0].ID > 0)
        {
            gunslot1.GetComponent<Image>().sprite = database.GetItemGun[gunInventory.Items[0].ID].gundata.img;
        }
        else
        {
            gunslot1.GetComponent<Image>().sprite = null;

        }
        if (gunInventory.Items[1].ID > 0)
        {
            gunslot2.GetComponent<Image>().sprite = database.GetItemGun[gunInventory.Items[1].ID].gundata.img;
        }
        else
        {
            gunslot2.GetComponent<Image>().sprite = null;
        }


        //this set of code is responsible for setting green underline beneath the gunSlot.

        if (GameManager.Instance.ActiveSlot == 1)
        {
            gunslot1.transform.GetChild(0).gameObject.SetActive(true);
            gunslot2.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            gunslot1.transform.GetChild(0).gameObject.SetActive(false);
            gunslot2.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnClick(int id)
    {
        Debug.Log("onclick");
        GameManager.Instance.ChangeActiveSlot(id);
        /*
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
                 if (Player.transform.childCount > 5)
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
         //ShowTickButton(obj.transform.position);
         gunEquipped = true;
     }*/
    }
}
