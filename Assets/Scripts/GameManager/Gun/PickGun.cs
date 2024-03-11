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
    public GunInventory inventory;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        PickGunButton = GameObject.Find("PickGun");
        if(PickGunButton != null)
        {
           // Debug.Log("Pickgun is not  null");
            PickGunButton.SetActive(false);
        }
        Player = GameObject.FindWithTag("Player");
        
        pickAction = playerInput.actions["PickGun"];

    }
    private void OnEnable()
    {
        //Debug.Log("enabling ...");
        if (pickAction == null) Debug.Log("pick gun null");

        pickAction.performed += _ => Pick();
        
    }
    private void OnDisable()
    {

        pickAction.performed -= _ => Pick();
       
    }
    private void Update()
    {

    }

    public void Pick()
    {

      
        GunItemObject gunitem = GameManager.Instance.collidedWeapon.GetComponent<InventoryItem>().GunItem;
        Item itemGenerated = new Item(gunitem);
        inventory.AddItem(itemGenerated, 1);
        Destroy(GameManager.Instance.collidedWeapon);
        GameManager.Instance.collidedWeapon = null;
        PickGunButton.SetActive(false);

        

    
       
    }

   
}
