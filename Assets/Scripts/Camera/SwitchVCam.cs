using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
public class SwitchVAam : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private PlayerInput playerInput;
    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;
    [SerializeField]
    private Canvas NormalCanvas;
    [SerializeField]
    private Canvas ZoomCanvas;

    void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
        ZoomCanvas.enabled = false;
        if(aimAction == null)
        {
            Debug.Log("aim action not found");
        }
    }

    private void OnEnable()
    {
        //Debug.Log("enabling ...");
        
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _=> CancelAim();
    }
    private void OnDisable()
    {
       
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }

    private void StartAim()
    {
        Debug.Log("called start Aim");
        NormalCanvas.enabled = false;
        ZoomCanvas.enabled = true;
        virtualCamera.Priority += 10;
    }
    private void CancelAim()
    {
        Debug.Log("cancel Aim");
        ZoomCanvas.enabled = false;
        NormalCanvas.enabled = true;
        
        virtualCamera.Priority -= 10;

    }

}
