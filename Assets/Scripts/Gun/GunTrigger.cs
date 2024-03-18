using UnityEngine;

public class GunTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject canvasButton; // Reference to the button on the canvas
   //public GameObject collidedGunPrefab; // Reference to the collided gun prefab
 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            // Enable the button on the canvas
            Debug.Log("setting collided weapon");
            canvasButton.SetActive(true);
            
            GameManager.Instance.collidedWeapon = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gun"))
        {
            // Disable the button on the canvas
            Debug.Log("setting collided weapon null");
            canvasButton.SetActive(false);
            GameManager.Instance.collidedWeapon = other.gameObject;
        }
    }


}
