/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.EventSystems;
public class TouchScreen : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction dragScreen;
    public CinemachineInputProvider inputProvider;
   // public CinemachineVirtualCamera cm1;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        dragScreen = playerInput.actions["DragScreen"];
    }

    public void OnEnable()
    {
        Debug.Log("enabling  touch panel");
        dragScreen.started += ctx => StartedDragging(ctx);
        dragScreen.performed += ctx => Dragging(ctx);
        dragScreen.canceled += ctx => StopDragging(ctx);
    }

    public void OnDisable()
    {
        dragScreen.started -= ctx => StartedDragging(ctx);
        dragScreen.performed -= ctx => Dragging(ctx);
        dragScreen.canceled -= ctx => StopDragging(ctx);
    }

    void StartedDragging(InputAction.CallbackContext ctx)
    {
        Debug.Log("Started Dragging");
        
    }

    void Dragging(InputAction.CallbackContext ctx)
    {
        inputProvider.enabled = true;
        float horizontalInput = ctx.ReadValue<Vector2>().x;
        float verticalInput = ctx.ReadValue<Vector2>().y;
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Do not execute drag action if touch is over a UI element
            inputProvider.enabled = false;
            Debug.Log("Touched on gameobject");
            return;
        }
       
        
    }

    void StopDragging(InputAction.CallbackContext ctx)
    {
        //inputProvider.enabled = true;
        Debug.Log("Stopped Dragging");
    }
}
*/