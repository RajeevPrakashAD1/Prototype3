using System.Collections;
using System.Collections.Generic;
using UnityEngine;




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
    private Transform mainCamera;
    public LayerMask terrainLayerMask;

    private void Awake()
    {
        PlayerInput = new PlayerControls();
        controller = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        PlayerInput.Enable();
    }

    private void OnDisable()
    {
        PlayerInput.Disable();
    }
    private void Start()
    {
        mainCamera = Camera.main.transform;
        
    }

    void Update()
    {
        /*groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }*/

        Vector2 movementInput = PlayerInput.Player.Move.ReadValue<Vector2>();
        Debug.Log("MovementInput" + movementInput);

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
           // transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        }
        // Changes the height position of the player..
        /*if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }*/

        /*playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);*/
    }
}