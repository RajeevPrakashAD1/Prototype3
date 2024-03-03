using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collided with object");
        //Debug.Log("player collided" + collision.gameObject);
        if(collision.transform.tag == "Inventory Item")
        {
            ItemObject data = collision.gameObject.GetComponent<InventoryItem>().item;
            Item ItemGenerate = new Item(data);
            inventory.AddItem(ItemGenerate,1);
            Destroy(collision.gameObject);

        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[9];

        // Initialize each InventorySlot object before accessing its properties
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            inventory.Container.Items[i] = new InventorySlot();
            inventory.Container.Items[i].item = new Item(); // Assuming Item is a class with a public constructor
            //inventory.Container.Items[i].item.Name = "ch" + i;
        }
    }
}
