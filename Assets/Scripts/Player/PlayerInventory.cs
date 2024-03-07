using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public PowerUpInventory inventory;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Inventory Item"))
        {
            ItemObject data = collision.gameObject.GetComponent<InventoryItem>().item;
            Item itemGenerated = new Item(data);
            inventory.AddItem(itemGenerated, 1);
            Destroy(collision.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        // No need to reinitialize the PowerUpItems array here.
        // It will be automatically initialized when the game starts.
    }
}
