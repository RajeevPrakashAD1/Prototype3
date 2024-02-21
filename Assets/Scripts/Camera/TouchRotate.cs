using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class TouchRotate : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float screenSplitRatio = 0.5f; // Split ratio for the screen (0.5 for half)

    private Vector2 previousTouchPosition;

    void Update()
    {
        if (Touchscreen.current.primaryTouch.isInProgress)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            if (touchPosition.x >= Screen.width * screenSplitRatio)
            {
                // Calculate touch delta
                Vector2 touchDelta = touchPosition - previousTouchPosition;
                Debug.Log("touching screen");


                // Apply the touch delta to the Cinemachine virtual camera
                virtualCamera.transform.Translate(-touchDelta.x * Time.deltaTime, -touchDelta.y * Time.deltaTime, 0);
            }
            previousTouchPosition = touchPosition;
        }
    }
}
