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

            InventoryItem data = collision.gameObject.GetComponent<InventoryItem>();
            if(data.type == "PowerUp")
            {
                Item itemGenerated = new Item(data.PowerUpitem);
                inventory.AddItem(itemGenerated, 1);
                Destroy(collision.gameObject);
            }
            
        }
    }

    
}
