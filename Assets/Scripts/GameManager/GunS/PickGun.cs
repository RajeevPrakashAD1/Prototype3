using UnityEngine;
using UnityEngine.InputSystem;

public class PickGun : MonoBehaviour
{
    public GameManager gm;
    public GameObject Player;
    [SerializeField]
    private PlayerInput playerInput;
    private InputAction pickAction;
    public GameObject PickGunButton;
    private GameObject NewGun;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        PickGunButton = GameObject.Find("PickGun");
        if(PickGunButton != null)
        {
            //Debug.Log("Pickgun is not  null");
            PickGunButton.SetActive(false);
        }
        Player = GameObject.FindWithTag("Player");
        
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
        gm.currentWeapon = NewGun;
        PickGunButton.SetActive(false);
       
    }

    private  void ChangeGun(GameObject newGun)
    {
        // Find the child object with the "Gun" tag
        Transform oldGunTransform = null;
        Debug.Log("player" + Player);
        if(Player != null) Debug.Log("player not null");
        if (Player && Player.transform && Player.transform.childCount > 1)
        {
            oldGunTransform = Player.transform.GetChild(1);
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
            newGun = instantiatedNewGun;
            
        }
        else
        {
            Debug.Log("No gun object found as a child of the player.");
         



        }
    }
}
