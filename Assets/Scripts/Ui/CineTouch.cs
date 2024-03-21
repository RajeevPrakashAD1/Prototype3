using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Cinetouch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cineCam;
    public CinemachineInputProvider pcam;
    [SerializeField] TouchPanel touchField;
    [SerializeField] float SenstivityX = 5f;
    [SerializeField] float SenstivityY = 5f;
    [SerializeField] float moveSpeed = 2f;
    public Transform CameraFollow;
    private PlayerInput playerInput;
    // Start is called before the first frame update
    void Awake()
    {
      
     

   }

    // Update is called once per frame
    void Update()
    {
        if (touchField.Pressed)
        {
            CameraFollow.Rotate(Vector3.up, touchField.TouchDist.x * SenstivityX * Time.deltaTime, Space.World);
            CameraFollow.Rotate(Vector3.right, -touchField.TouchDist.y * SenstivityY * Time.deltaTime, Space.Self);

            // Move the CameraFollow object in the direction of drag (x and y only)
            Vector3 moveDirection = new Vector3(touchField.TouchDist.x,0, touchField.TouchDist.y).normalized;
            CameraFollow.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.Self);
        }
    }
}