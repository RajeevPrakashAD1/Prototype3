using UnityEngine;
using UnityEngine.InputSystem;

public class PickGun : MonoBehaviour
{
    
   

    private PlayerInput playerInput;
    private InputAction pickAction;
    public GameObject PickGunButton;
    private GameObject NewGun;
    public GunInventory inventory;
    public GameObject Slot1;
    public GameObject Slot2;

    public GameObject GunParent;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        PickGunButton = GameObject.Find("PickGun");
        if(PickGunButton != null)
        {
           // Debug.Log("Pickgun is not  null");
            PickGunButton.SetActive(false);
        }
      
        
        pickAction = playerInput.actions["PickGun"];

    }
    private void OnEnable()
    {
        //Debug.Log("enabling ...");
        if (pickAction == null) Debug.Log("pick gun null");

        pickAction.performed += _ => AddToInventory();
        
    }
    private void OnDisable()
    {

        pickAction.performed -= _ => AddToInventory();
       
    }
    private void Update()
    {
        //Pick();
    }
    public void AddToInventory()
    {
        PickGunButton.SetActive(false);
        GunItemObject gunItem = GameManager.Instance.collidedWeapon.GetComponent<InventoryItem>().GunItem;
        Item itemGenerated = new Item(gunItem);
        inventory.AddItem(itemGenerated, 1, GameManager.Instance.collidedWeapon);

        GameObject collidedWeapon = GameManager.Instance.collidedWeapon;

        if (GameManager.Instance.ActiveSlot == 1)
        {
            if (Slot1.transform.childCount > 0)
            {
                Transform child = Slot1.transform.GetChild(0);
                child.SetParent(GunParent.transform);
                Rigidbody rb = child.GetComponent<Rigidbody>();
               /* if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }*/
                GunMain gunMain = child.GetComponent<GunMain>();
                if (gunMain != null)
                {
                    gunMain.enabled = false;
                }
            }
            GunMain gunMain2 = collidedWeapon.GetComponent<GunMain>();
            gunMain2.enabled = true;
            gunMain2.equipped = true;
            collidedWeapon.transform.SetParent(Slot1.transform);
            collidedWeapon.GetComponent<GunMain>().enabled = true;
            collidedWeapon.transform.localPosition = Vector3.zero;
            collidedWeapon.transform.localRotation = Quaternion.identity;

        }
        else
        {
            if (Slot2.transform.childCount > 0)
            {
                Transform child = Slot2.transform.GetChild(0);
                child.SetParent(GunParent.transform);
                Rigidbody rb = child.GetComponent<Rigidbody>();
               /* if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }*/
                GunMain gunMain = child.GetComponent<GunMain>();
                if (gunMain != null)
                {
                    gunMain.enabled = false;
                }
            }

            collidedWeapon.transform.SetParent(Slot2.transform);
            GunMain gunMain2 = collidedWeapon.GetComponent<GunMain>();
            gunMain2.enabled = true;
            gunMain2.equipped = true;
            collidedWeapon.transform.localPosition = Vector3.zero;
            collidedWeapon.transform.localRotation = Quaternion.identity;

        }
    }

    public void Pick()
    {
        if(GameManager.Instance.ActiveSlot == 1)
        {
            //make slot2 child component mesh render off and gunmain script off and slot1 on
            Slot2.SetActive(false);
            Slot1.SetActive(true);
        }
        else
        {
            ////make slot1 child component mesh render off and gunmain script off and slot2 on
            ///  Slot1.SetActive(false);
            Slot1.SetActive(false);
            Slot2.SetActive(true);
        }
    }
    public void Drop(int id)
    {
        if(id == 1)
        {
            Transform dropgun = Slot1.transform.GetChild(0);
            dropgun.SetParent(GunParent.transform);
            dropgun.position += new Vector3(0, -3f, 0);

        }
        if (id == 2)
        {
            Transform dropgun = Slot2.transform.GetChild(0);
            dropgun.SetParent(GunParent.transform);
            dropgun.position += new Vector3(0, -3f, 0);
        }
    }

   
}
