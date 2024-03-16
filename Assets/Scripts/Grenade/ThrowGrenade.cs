using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class ThrowGrenade : MonoBehaviour
{
    // Start is called before the first frame update
    
    private PlayerInput playerInput;
    private InputAction GrenadeShootAction;
   
    //public BulletInventory inventory;

    private float throwForce = 10f;
    private float maxForce =30f;
    private Vector3 initialThrowDirection = new Vector3(0, 1f, 0);
    [SerializeField] private Transform shootPoint;
    public GameObject grenadePrefab;
    public LineRenderer trajectoryLine;


    private Transform mainCamera;
 
    private int currentAmmo = 1; // Current ammo count
    private bool isHolding;

    private float holdDuration = 0f;
    public TextMeshProUGUI tm;

    
    private void Awake()
    {
        mainCamera = Camera.main.transform;
        playerInput = GetComponent<PlayerInput>();
        tm.text = currentAmmo.ToString();
        if (playerInput != null)
        {
            GrenadeShootAction = playerInput.actions["Grenade"];
            

        }
        else
        {
            Debug.LogWarning("PlayerInput component not found.");
        }

    }

    private void Update()
    {
        if (isHolding)
        {
            holdDuration += Time.deltaTime;
           // Debug.Log("Button held for: " + holdDuration + " seconds");
            ChargeThrow();
        }
    }
    private void Start()
    {
        
        GrenadeShootAction.started += _ => OnHoldStarted();
        GrenadeShootAction.canceled += _ => OnHoldCanceled();
     }

    private void Destroy()
    {
        GrenadeShootAction.started -= _ => OnHoldStarted();
        GrenadeShootAction.canceled -= _ => OnHoldCanceled();

    }

    void OnHoldStarted()
    {
        Debug.Log("start");
        if (currentAmmo <= 0) return;
        isHolding = true;
        holdDuration = 0f;
        trajectoryLine.enabled = true;
       // ChargeThrow();

    }
    void OnHoldCanceled()
    {
        isHolding = false;
        trajectoryLine.enabled = false;
        ShootGrenade(holdDuration);
        
    }
    void ChargeThrow()
    {
        Vector3 gv = (mainCamera.transform.forward + initialThrowDirection).normalized * Mathf.Min(holdDuration * throwForce, maxForce);
        Vector3 origin = shootPoint.position + shootPoint.forward;
        Vector3[] points = new Vector3[100];
        trajectoryLine.positionCount = points.Length;
        for(int i = 0; i < points.Length; i++)
        {
            float time = i*0.1f;
            points[i] = origin + gv * time + 0.5f * Physics.gravity * time * time;

        }
        trajectoryLine.SetPositions(points);
    }

    private void ShootGrenade(float holdDuration)
    {
        Debug.Log("shooting grenade");
        if (grenadePrefab != null && currentAmmo > 0)
        {
            // Instantiate the grenade prefab at the shoot point
            Vector3 spawnPosition = shootPoint.position + mainCamera.forward;
            GameObject grenade = Instantiate(grenadePrefab, spawnPosition, mainCamera.rotation);

            // Get the Rigidbody component of the grenade
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.Log("rb is null...");
            }
            else
            {
                Debug.Log(rb);
            }

            Vector3 finalDirection = (mainCamera.forward + initialThrowDirection).normalized;
            float finalForce = Mathf.Min(throwForce * holdDuration, maxForce);
           // Debug.Log("final force" + finalForce);
            rb.AddForce(finalDirection* finalForce, ForceMode.VelocityChange);

           

            // Reduce ammo count
            currentAmmo--;
            tm.text = currentAmmo.ToString();
        }
    }


    public void IncGrenade()
    {
        currentAmmo += 1;
        tm.text = currentAmmo.ToString();

    }

}
