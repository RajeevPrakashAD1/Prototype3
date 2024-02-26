using UnityEngine;
using UnityEngine.InputSystem;

public class PickGun : MonoBehaviour
{
    public GameManager gm;
    public GameObject Player;
    [SerializeField]
    private PlayerInput playerInput;
    private InputAction pickAction;
    public GameObject canvasButton;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pickAction = playerInput.actions["PickGun"];
    }
    private void OnEnable()
    {
        //Debug.Log("enabling ...");
        if (pickAction == null) Debug.Log("pick gun null");

        pickAction.performed += _ => EquipGun();
        
    }
    private void OnDisable()
    {

        pickAction.performed -= _ => EquipGun();
       
    }
    private void Update()
    {

    }

    public void EquipGun()
    {
        Debug.Log("equipping");
        gm.currentWeapon = gm.collidedWeapon;
        ChangeGun(gm.currentWeapon);
        gm.currentWeapon.SetActive(false);
        
        canvasButton.SetActive(false);
       
    }

    private  void ChangeGun(GameObject newGun)
    {
        // Find the child object with the "Gun" tag
        Transform oldGunTransform = null;
        if (Player.transform.childCount > 1)
        {
            oldGunTransform = Player.transform.GetChild(1);
        }
        for (int i = 0; i < Player.transform.childCount; i++)
        {
            // Get the child transform at index i
            Transform childTransform = Player.transform.GetChild(i);

            // Log the name of the child object
           // Debug.Log("Child Object tag: "+ i + " " + childTransform.tag);
        }

        //Debug.Log("old gun" + oldGunTransform.position);
        if (oldGunTransform != null)
        {
            // Save the position and rotation of the old gun
            Vector3 oldGunPosition = oldGunTransform.position;
            Quaternion oldGunRotation = oldGunTransform.rotation;

            // Destroy the old gun
           // Debug.Log("destroying old gun");
            Destroy(oldGunTransform.gameObject);

            // Create a new gun object and set its position and rotation
            GameObject instantiatedNewGun = Instantiate(newGun, oldGunPosition, oldGunRotation, Player.transform);
            instantiatedNewGun.tag = "Gun";
            instantiatedNewGun.layer = 12;
            GunMain gunMain = instantiatedNewGun.GetComponent<GunMain>();
            gunMain.equipped = true;
            
        }
        else
        {
            Debug.Log("No gun object found as a child of the player.");
            Vector3 oldGunPosition = new Vector3(0.5599976f, 0f, 0.590004f);
            Quaternion oldGunRotation = new Quaternion(0f, 0f, 0f, 0f);
            GameObject instantiatedNewGun = Instantiate(newGun, oldGunPosition, oldGunRotation, Player.transform);
            instantiatedNewGun.tag = "Gun";
            GunMain gunMain = instantiatedNewGun.GetComponent<GunMain>();
            gunMain.equipped = true;



        }
    }
}
