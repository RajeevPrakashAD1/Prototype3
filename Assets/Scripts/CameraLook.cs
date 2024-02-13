using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
[RequireComponent(typeof(CinemachineFreeLook))]

public class CameraLook : MonoBehaviour
{
    // Start is called before the first frame update
    private CinemachineFreeLook cineMachine;
    private PlayerControls PlayerInput;
    [SerializeField]
    private float lookSpeed = 1f;
    private void Awake()
    {
        PlayerInput = new PlayerControls();
        cineMachine = GetComponent<CinemachineFreeLook>();
    }
    private void OnEnable()
    {
        PlayerInput.Enable();
    }

    private void OnDisable()
    {
        PlayerInput.Disable();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 delta = PlayerInput.Player.Look.ReadValue<Vector2>();
        cineMachine.m_XAxis.Value += delta.x*200 * lookSpeed * Time.deltaTime;
        cineMachine.m_YAxis.Value += delta.y * lookSpeed * Time.deltaTime;

    }
}
