using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController),typeof(PlayerInput))]

public class PlayerMovement : MonoBehaviour
{
    
    private Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 12.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float terrainFollowSpeed = 200f;
    private CharacterController controller;

    private PlayerControls PlayerInput;
    private PlayerInput playerInput;
    private Transform mainCamera;
    public LayerMask terrainLayerMask;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject shootPoint;
    [SerializeField]
    private GameObject bulletParent;


    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction shootAction;
    private InputAction aimAction;

    private void Awake()
    {
        PlayerInput = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();

        //getting player inputs for using unity new input manager
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        shootAction = playerInput.actions["Shoot"];

    }
    private void OnEnable()
    {
        PlayerInput.Enable();
        shootAction.performed += _ => ShootGun();
    }

    private void OnDisable()
    {
        PlayerInput.Disable();
        shootAction.performed -= _ => ShootGun();
    }
    private void Start()
    {
        mainCamera = Camera.main.transform;
        
    }
    public void ShootGun()
    {
       
        //instantiate bullet    
      
        bullet.transform.position =new Vector3(0, 0, 0);
        GameObject bullett = Instantiate(bullet, shootPoint.transform.position, Quaternion.identity, bulletParent.transform);


        BulletCtrl bulletCtrl = bullett.GetComponent<BulletCtrl>();


        //using raycast we can check where the bullet is hitting and set targer and hit true for bulletCtrl.
        RaycastHit hit;


        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, Mathf.Infinity))
        {

            bulletCtrl.target = hit.point;
            bulletCtrl.hit = true;

        }
        else
        {

            bulletCtrl.target = mainCamera.position + mainCamera.forward * 25f;
            bulletCtrl.hit = false;
        }
    }
    void Update()
    {
       

        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        
        
       
        Vector3 move = (mainCamera.forward*movementInput.y + mainCamera.right * movementInput.x);
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        //Debug.Log("trying to hit");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, terrainLayerMask))
        {
            // Adjust player's position to match terrain height

            Vector3 targetPosition = hit.point + Vector3.up * controller.height / 2f;
            //Debug.Log("adjusting height" + hit.point);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * terrainFollowSpeed);
           

        }
      
    }
}