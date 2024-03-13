using System.Collections;
using UnityEngine;

public class InventoryInitializer : MonoBehaviour
{
    public GameObject InventoryParent;

    void Start()
    {
        //InitializeInventories();
        if(InventoryParent != null)
        {
            StartCoroutine(DeactivateObjectAfterDelay());
        }
    }

    private void OnApplicationQuit()
    {
        
    }

    IEnumerator DeactivateObjectAfterDelay()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.01f);

        // Deactivate the target object
        InventoryParent.SetActive(false);
    }

}
  
