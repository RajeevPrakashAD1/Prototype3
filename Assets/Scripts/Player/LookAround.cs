/*using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Cinemachine;

public class LookAround : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimPOV1;
    [SerializeField] private CinemachineVirtualCamera aimPOV2;
    [SerializeField] private Transform aimTarget; // The target object to control the aim

    [SerializeField] private float _touchSensitivityX = 1f;
    [SerializeField] private float _touchSensitivityY = 1f;
    private PlayerInput playerInput;
    private InputAction touchPositionAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        touchPositionAction = playerInput.actions["TouchScreen"];
    }

    private void OnEnable()
    {
        touchPositionAction.performed += Move;
    }
    private void OnDisable()
    {
        touchPositionAction.performed -= Move;
    }

    private void Move(InputAction.CallbackContext context)
    {
        *//* if(Touchscreen.current == null)
         {
             Debug.Log("touchscreen is null");
             return;
         }
         if (Touchscreen.current.touches.Count == 0)
             return;*//*
        Debug.Log("coming...");
        if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[0].touchId.ReadValue()))
        {
            if (Touchscreen.current.touches.Count > 1 && Touchscreen.current.touches[1].isInProgress)
            {
                if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[1].touchId.ReadValue()))
                    return;

                Vector2 touchDeltaPosition = Touchscreen.current.touches[1].delta.ReadValue();
                AdjustAim(touchDeltaPosition);
            }
        }
        else
        {
            if (Touchscreen.current.touches.Count > 0 && Touchscreen.current.touches[0].isInProgress)
            {
                if (EventSystem.current.IsPointerOverGameObject(Touchscreen.current.touches[0].touchId.ReadValue()))
                    return;

                Vector2 touchDeltaPosition = Touchscreen.current.touches[0].delta.ReadValue();
                AdjustAim(touchDeltaPosition);
            }
        }
    }

    private void AdjustAim(Vector2 touchDeltaPosition)
    {
        CinemachineVirtualCamera activeCamera = aimPOV1.gameObject.activeSelf ? aimPOV1 : aimPOV2;

        // Calculate aim adjustments based on touch input
        float horizontalAdjustment = touchDeltaPosition.x * _touchSensitivityX;
        float verticalAdjustment = touchDeltaPosition.y * _touchSensitivityY;

        // Rotate aim target based on touch input
        aimTarget.Rotate(Vector3.up, horizontalAdjustment, Space.World);
        aimTarget.Rotate(Vector3.right, verticalAdjustment, Space.World);

        // Update Cinemachine virtual camera's LookAt target
        activeCamera.LookAt = aimTarget;
    }
}
*/