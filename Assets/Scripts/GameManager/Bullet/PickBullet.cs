using UnityEngine;
using UnityEngine.InputSystem;

public class PickBullet: MonoBehaviour
{
    public GameManager gm;
    public GameObject Player;
    [SerializeField]
    private PlayerInput playerInput;
    private InputAction pickAction;
    public GameObject PickGunButton;
    private GameObject NewGun;
    public BulletInventory inventory;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
       
        Player = GameObject.FindWithTag("Player");
        
        pickAction = playerInput.actions["PickGun"];

    }
  
    private void Update()
    {

    }

    public void Pick(BulletItemObject obj)
    {

      
       
        Item itemGenerated = new Item(obj);
        inventory.AddItem(itemGenerated, 100);

       
        

    
       
    }

    
    public void OnTriggerEnter(Collider other)
    {
       
    }
    public void OnCollisionEnter(Collision other)
    {
        //Debug.Log("collided"+other.gameObject.tag);
        if (other.gameObject.tag == "GroundBullet")
        {
            BulletItemObject bi = other.gameObject.GetComponent<InventoryItem>().BulletItem;
            Pick(bi);
            Destroy(other.gameObject);
        }
    }

}
