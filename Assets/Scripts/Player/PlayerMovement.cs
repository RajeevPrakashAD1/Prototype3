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
    private float playerSpeed;
    
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;
    [SerializeField]
    private float terrainFollowSpeed = 200f;
    private CharacterController controller;

    //private PlayerControls PlayerInput;
    private PlayerInput playerInput;
    private Transform mainCamera;
    public LayerMask terrainLayerMask;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private GameObject shootPoint;
    [SerializeField]
    private GameObject bulletParent;
    [SerializeField]
    private float rotationSpeed = 1f;
   

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction shootAction;
    private InputAction aimAction;

    private void Awake()
    {
        //PlayerInput = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();

        //getting player inputs for using unity new input manager
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        shootAction = playerInput.actions["Shoot"];

    }
   
    private void Start()
    {
        mainCamera = Camera.main.transform;
        

        
    }
  
    void Update()
    {

        playerSpeed = GameManager.Instance.PlayerSpeed;
        /*Vector2 movementInput = moveAction.ReadValue<Vector2>();
        playerSpeed = GameManager.Instance.PlayerSpeed;


        Vector3 move = (mainCamera.forward*movementInput.y + mainCamera.right * movementInput.x);
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }*/
        Vector2 movementInput = moveAction.ReadValue<Vector2>();

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);


        move = move.x * mainCamera.right.normalized + move.z * mainCamera.forward.normalized;
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        float targetAngle = mainCamera.eulerAngles.y;
        Quaternion Targetrotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, Targetrotation, rotationSpeed * Time.deltaTime);
            
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