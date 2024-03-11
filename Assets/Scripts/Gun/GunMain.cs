using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GunMain : MonoBehaviour
{
   
    public GunData gunData;
    private PlayerInput playerInput;
    private InputAction shootAction;
    private InputAction reloadAction;
    public BulletInventory inventory;
    

    [SerializeField] private GameObject shootPoint;
    public GameObject bulletPrefab;
    public int bulletHoldingId;

    public BulletManager bmng;


    private Transform mainCamera;
    private bool canShoot = true; // Control fire rate
    private float nextShootTime = 0f; // Time for next allowed shot
    private int currentAmmo=0; // Current ammo count
    private int maxAmmo; // Maximum ammo count (magazine size)
    private float reloadTime; // Reload time
    public bool equipped;
    public Text reloadText;
   

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            shootAction = playerInput.actions["Shoot"];
            reloadAction = playerInput.actions["Reload"];

        }
        else
        {
            Debug.LogWarning("PlayerInput component not found.");
        }
        if (gunData == null)
        {
            gunData = GetComponent<InventoryItem>().GunItem.gundata;
        }
        mainCamera = Camera.main.transform;

        // Initialize ammo values based on gun data
        maxAmmo = gunData.magazineSize;
        
        currentAmmo = maxAmmo;
       // Debug.Log("maxammo " + maxAmmo + " " + currentAmmo);
       
        reloadTime = gunData.reloadTime;
        

        
    }
    private void Update()
    {

        if (equipped) chooseBullet();
       
    }
    public void chooseBullet()
    {
        if (!equipped) return;
        // BulletManager bmng = bulletParent.GetComponent<BulletManager>();
        
        
        bulletPrefab = bmng.bullet;
        bulletHoldingId = bmng.BulletHoldingid;
      //  Debug.Log(this.gameObject + "  choosing bullet" + bmng.bullet + " " + bmng.BulletHoldingid + " " + bulletHoldingId);
        if (inventory == null)
        {
            inventory = bmng.bulletInventory;
        }
        
    }

    private void OnEnable()
    {
        shootAction.performed += _ => ShootGun();
        reloadAction.performed += _ => Reload();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => ShootGun();
        reloadAction.performed -= _ => Reload();
    }
    private void Start()
    {
        GameObject reloadTextObject = GameObject.FindWithTag("ReloadingText");


        // Check if the GameObject is found
        if (reloadTextObject != null)
        {
            // Get the Text component from the GameObject
            reloadText = reloadTextObject.GetComponent<Text>();
        }

        

    }



    private void ShootGun()
    {
        if (!equipped)
        {
            reloadText.text = "Reloading...";
            return;
        }
        Debug.Log("gameobject being called " + this.gameObject);
       //if(equipped) Debug.Log("equipped" + equipped + " " + currentAmmo +"maxammo "+ maxAmmo + " "+ nextShootTime +" "+Time.time);
             
        if (equipped && canShoot && Time.time >= nextShootTime && bulletPrefab != null)
        {
            //Debug.Log("initiating bullet");
            if (currentAmmo > 0)
            {
               


                GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, Quaternion.identity);
                BulletCtrl bulletCtrl = bullet.GetComponent<BulletCtrl>();

                // Set bullet damage
                bulletCtrl.damage = gunData.damage;
                // Define layers to exclude
                int layer1 = LayerMask.NameToLayer("Player");
                int layer2 = LayerMask.NameToLayer("PlayerProtection");
                int layer3 = LayerMask.NameToLayer("PlayerGun");
                int layer4 = LayerMask.NameToLayer("Enemy");
                int layer5 = LayerMask.NameToLayer("BigEnemy");

                // Combine layers using bitwise OR operations
                int excludedLayers = (1 << layer1) | (1 << layer2) | (1 << layer3)  ;

                // Invert the combined mask to exclude specified layers
                int layerMask = ~excludedLayers;

              RaycastHit hit;
                if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, 300f,layerMask))
                {
                    Debug.Log("hit : " + hit.collider.gameObject +" "+ hit.point);
                    bulletCtrl.target = hit.point;
                  bulletCtrl.hit = true;
                }
                else
                {
                    bulletCtrl.target = mainCamera.position + mainCamera.forward * 55f;
                   bulletCtrl.hit = false;
                }

                // Reduce ammo count
                currentAmmo--;

                // Control fire rate
                nextShootTime = Time.time + 1f / gunData.fireRate;
                // Debug.Log("making can shoot false");
                canShoot = false;
                Invoke("ResetShoot", 1f / gunData.fireRate);
                reloadText.text = currentAmmo.ToString();
            }
            else
            {
                Reload();
            }
        }
        else
        {   if (!equipped) reloadText.text = "No Gun";
            else if(bulletPrefab == null || currentAmmo <= 0) reloadText.text = "No bullet";
        }
    }

    private void ResetShoot()
    {
        //Debug.Log("making reshoot true");
        canShoot = true;
    }

    public void Reload()
    {
        if (!equipped) return;
        // Reload logic
        if (reloadText) reloadText.text = "Realoading....";
        int totalAmmo;
        for(int i=0;i< inventory.Items.Length; i++)
        {
            if(inventory.Items[i].ID == bulletHoldingId)
            {
                totalAmmo = inventory.Items[i].amount;
                int bulletToReload = Mathf.Min(totalAmmo, maxAmmo);
                inventory.Items[i].amount -= (bulletToReload-currentAmmo);
                currentAmmo = bulletToReload;
                
            }
        }

        
        Debug.Log("Reloading..."+currentAmmo+ " "+ maxAmmo);
        Invoke("ResetReload", reloadTime); // Reset reload time
        canShoot = false;
    }

    private void ResetReload()
    {

        Debug.Log("Reload complete.");
        canShoot = true;
        if (reloadText) reloadText.text = currentAmmo.ToString();
    }
}
