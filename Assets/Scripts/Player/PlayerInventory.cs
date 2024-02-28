using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collided with object");
        if(collision.transform.tag == "Inventory Item")
        {
            ItemObjects data = collision.gameObject.GetComponent<InventoryItem>().item;
            inventory.AddItem(data,1);
            Destroy(collision.gameObject);

        }
    }

    private void OnApplicationQuit()
    {
        //inventory.Container.Clear();
    }
}
