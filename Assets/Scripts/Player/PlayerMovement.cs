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
    private float playerSpeed = 2.0f;
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
    void ShootGun()
    {
        GameObject bullett = Instantiate(bullet, shootPoint.transform.position, Quaternion.identity, bulletParent.transform);
        //Debug.Log("hit infor " + hit.point);
        BulletCtrl bulletCtrl = bullett.GetComponent<BulletCtrl>();
        RaycastHit hit;
        
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, Mathf.Infinity))
        {
            
            bulletCtrl.target = hit.point;
            bulletCtrl.hit = true;

        }
        else
        {
            //Debug.Log("not hitting anything ....");
            bulletCtrl.target = mainCamera.position + mainCamera.forward * 25f;
            bulletCtrl.hit = true;
        }
    }
    void Update()
    {
        /*groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }*/

        //Vector2 movementInput = PlayerInput.Player.Move.ReadValue<Vector2>();

        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        
        
        //Debug.Log("MovementInput" + movementInput);

        //Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 move = (mainCamera.forward*movementInput.y + mainCamera.right * movementInput.x);
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, terrainLayerMask))
        {
            // Adjust player's position to match terrain height
            Vector3 targetPosition = hit.point + Vector3.up * controller.height / 2f;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * terrainFollowSpeed);
           //transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        }
        // Changes the height position of the player..
        /*if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }*/

        /*playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);*/


        //
    }
}