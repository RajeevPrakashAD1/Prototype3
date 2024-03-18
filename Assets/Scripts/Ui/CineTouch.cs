using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Cinetouch : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cineCam;
    public CinemachineInputProvider pcam;
    [SerializeField] TouchPanel touchField;
    [SerializeField] float SenstivityX = 2f;
    [SerializeField] float SenstivityY = 2f;
    public GameObject player;
    private PlayerInput playerInput;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = player.GetComponent<PlayerInput>();
        if(playerInput != null)
        {
            Debug.Log("player input not null in cinetouh");
            Debug.Log("cinmachine input provide check xyaxis " + pcam.XYAxis);
            playerInput.enabled = false;
            playerInput.enabled = true;
        }

   }

    // Update is called once per frame
    void Update()
    {
        //cineCam. += touchField.TouchDist.x * SenstivityX * Time.deltaTime;
        //cineCam.m_YAxis.Value += touchField.TouchDist.y * SenstivityY * Time.deltaTime;
    }
}