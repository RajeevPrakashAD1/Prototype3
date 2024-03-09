using UnityEngine;

public class InventoryInitializer : MonoBehaviour
{
    public PowerUpInventory powerUpInventoryPrefab; // Assign in the inspector
    public GunInventory gunInventoryPrefab; // Assign in the inspector

    void Start()
    {

    }

    private void OnApplicationQuit()
    {
        InitializeInventories();
    }

    public void InitializeInventories()
    {
        powerUpInventoryPrefab.Items = new InventorySlot[6];
        gunInventoryPrefab.Items = new InventorySlot[4];
    }

}
  
