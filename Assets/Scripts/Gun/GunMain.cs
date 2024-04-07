using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;
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

    //public BulletManager bmng;


    private Transform mainCamera;
    private bool canShoot = true; // Control fire rate
    private float nextShootTime = 0f; // Time for next allowed shot
    private int currentAmmo=0; // Current ammo count
    private int maxAmmo; // Maximum ammo count (magazine size)
    private float reloadTime; // Reload time
    public bool equipped;
    public Text reloadText;
    private Vector3 originalGunPosition;
    private Quaternion originalGunRotation;
    private bool isRecoiling;
    private bool shoot;
    public Vector3 originalCameraPosition;
    public CinemachineImpulseSource impulseSourse;
    public HandleBulletInventory hbi;
    public bool OnceEnabled;
    public Animator animator;
    public GameObject explosionPrefab;
    //public GameObject bulletParent; 

    private void Awake()
    {

       // Debug.Log("awaking gun main");
        playerInput = GetComponent<PlayerInput>();
        impulseSourse = GetComponent<CinemachineImpulseSource>();
        hbi = GameObject.FindGameObjectWithTag("BulletParent").GetComponent<HandleBulletInventory>();
        GameObject reloadTextObject = GameObject.FindWithTag("ReloadingText");
       


        // Check if the GameObject is found
        if (reloadTextObject != null)
        {
            // Get the Text component from the GameObject
            reloadText = reloadTextObject.GetComponent<Text>();
        }

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
        

        Reload();
    }
    private void Update()
    {

        if (equipped) chooseBullet();

        if (shoot)
        {
            ShootGun();
            GameManager.Instance.shooting = true;
        }
        else
        {
            GameManager.Instance.shooting = false;
        }
    }
    public void chooseBullet()
    {
        if (!equipped) { SetGun(); return; }
        // BulletManager bmng = bulletParent.GetComponent<BulletManager>();
        if (bulletPrefab != null) return;
        (GameObject _bulletPrefab, int _bulletHoldingId) = hbi.ChooseBullet(gunData.bulletType);
        bulletPrefab = _bulletPrefab;
        bulletHoldingId = _bulletHoldingId;
        SetGun();
        /*bulletPrefab = bmng.bullet;
        bulletHoldingId = bmng.BulletHoldingid;
      //  Debug.Log(this.gameObject + "  choosing bullet" + bmng.bullet + " " + bmng.BulletHoldingid + " " + bulletHoldingId);
        if (inventory == null)
        {
            inventory = bmng.bulletInventory;
        }*/

    }

    private void OnEnable()
    {
       // Debug.Log("enabling....gun script");

        shootAction.started += _ => ShootStart();
        shootAction.canceled += _ => OverShoot();
        reloadAction.performed += _ => Reload();
        maxAmmo = gunData.magazineSize;
        if (OnceEnabled == false)
        {
            currentAmmo = maxAmmo;
            // Debug.Log("maxammo " + maxAmmo + " " + currentAmmo);

            reloadTime = gunData.reloadTime;
            //originalGunPosition = transform.localPosition;
            originalGunRotation = transform.localRotation;
            OnceEnabled = true;
        }
        SetGun();
    }

    private void OnDisable()
    {
        shootAction.started -= _ => ShootStart();
        shootAction.canceled -= _ => OverShoot();
        reloadAction.performed -= _ => Reload();
    }

    public void ShootStart()
    {
        originalCameraPosition = Camera.main.transform.position;
        shoot = true;
    }
    public void OverShoot()
    {
        shoot = false;
      
        //StartCoroutine(RecoverFromRecoil());

    }
    private void Start()
    {
        

        

    }

    private void SetGun()
    {
        if (!equipped) { reloadText.text = "No Gun"; return; }
        if (bulletPrefab == null) reloadText.text = "Find bullet " + gunData.bulletType + "mm";
        else
        {
            reloadText.text = currentAmmo.ToString();
        }
    }

    private void ShootGun()
    {
       // Debug.Log("shooting");
        if (!equipped)
        {
            reloadText.text = "No Gun";
            return;
        }
      // Debug.Log("gameobject being called " + this.gameObject);
       //if(equipped) Debug.Log("equipped" + equipped + " " + currentAmmo +"maxammo "+ maxAmmo + " "+ nextShootTime +" "+Time.time);
             
        if (equipped && canShoot && Time.time >= nextShootTime && bulletPrefab != null)
        {
            //Debug.Log("initiating bullet");
            if (currentAmmo > 0)
            {
                
                //ApplyRecoil();
                

                GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, Quaternion.identity);
                GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(explosion, 0.1f);
                BulletCtrl bulletCtrl = bullet.GetComponent<BulletCtrl>();

                Rigidbody rb = bullet.GetComponent<Rigidbody>();


                // Set bullet damage
                bulletCtrl.damage = gunData.damage+bulletCtrl.bulletData.damage;
                // Define layers to exclude
                int layer1 = LayerMask.NameToLayer("Player");
                int layer2 = LayerMask.NameToLayer("PlayerProtection");
                int layer3 = LayerMask.NameToLayer("PlayerGun");
                int layer4 = LayerMask.NameToLayer("Enemy");
                int layer5 = LayerMask.NameToLayer("BigEnemy");

                // Combine layers using bitwise OR operations
                int excludedLayers = (1 << layer1) | (1 << layer2) | (1 << layer3);

                // Invert the combined mask to exclude specified layers
                int layerMask = ~excludedLayers;

                RaycastHit hit;
                if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, 500f, layerMask))
                {
                    /*  Debug.Log("hit : " + hit.collider.gameObject + " " + hit.point);
                      bulletCtrl.target = hit.point;
                      bulletCtrl.hit = true;*/
                   // Debug.Log("hit : " + hit.collider.gameObject + " " + hit.point);
                    Vector3 direction = hit.point - (bullet.transform.position);
                    Vector3 bfd = shootPoint.transform.position - hit.point;
                    bullet.transform.rotation = Quaternion.LookRotation(bfd.normalized);
                    if (rb != null)
                    {
                        rb.AddForce(direction.normalized * (500 *bulletCtrl.bulletData.speed));
                    }

                }
                else
                {
                    /* bulletCtrl.target = mainCamera.position + mainCamera.forward * 55f;
                     bulletCtrl.hit = false;*/
                    rb.AddForce(mainCamera.forward * (500 * bulletCtrl.bulletData.speed));
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
            else if(bulletPrefab == null || currentAmmo <= 0) reloadText.text = "Find bullet "+ gunData.bulletType+"mm";
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

        
        //Debug.Log("Reloading..."+currentAmmo+ " "+ maxAmmo);
        Invoke("ResetReload", reloadTime); // Reset reload time
        canShoot = false;
    }

    private void ResetReload()
    {

        Debug.Log("Reload complete.");
        canShoot = true;
        if (reloadText) reloadText.text = currentAmmo.ToString();
    }

    private void ApplyRecoil()
    {
        Debug.Log("applying recoid");
        if (!isRecoiling)
        {
            isRecoiling = true;
            
            // Apply jittery recoil effect
            float randomRecoil = gunData.recoil;
            Vector3 recoilOffset = new Vector3(0f, 0f, -randomRecoil);
            originalGunPosition = transform.localPosition;
            transform.localPosition += recoilOffset;
            //mainCamera.localPosition += recoilOffset*2;
            //impulseSourse.GenerateImpulseWithForce(gunData.recoil);
            // Start recovering from recoil after a delay
            StartCoroutine(RecoverFromRecoil());
        }
        originalGunRotation = transform.localRotation;
        transform.localRotation *= Quaternion.Euler(Vector3.up * gunData.recoil);
    }
    private IEnumerator RecoverFromRecoil()
    {
        Debug.Log("applying recover from recoil");
        // Delay before recovering from recoil
        yield return new WaitForSeconds(0.5f); // Adjust as needed
        isRecoiling = false;
        // Bring the gun back to its original position and rotation
        while (transform.localPosition != originalGunPosition || transform.localRotation != originalGunRotation)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalGunPosition, Time.deltaTime * gunData.recoilRecoverySpeed);
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, originalGunRotation, Time.deltaTime * gunData.recoilRecoverySpeed);
            //mainCamera.localPosition = Vector3.MoveTowards(mainCamera.localPosition, originalCameraPosition, Time.deltaTime * gunData.recoilRecoverySpeed);
            yield return null;
        }
    }
}
