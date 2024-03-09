using UnityEngine;

public class GunTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject canvasButton; // Reference to the button on the canvas
   //public GameObject collidedGunPrefab; // Reference to the collided gun prefab
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            // Enable the button on the canvas
            canvasButton.SetActive(true);
            
            gameManager.collidedWeapon = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            // Disable the button on the canvas
            canvasButton.SetActive(false);
            gameManager.collidedWeapon = null;
        }
    }


}
