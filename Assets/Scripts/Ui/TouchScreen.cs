/*using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TouchPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    // Flag to check if touch is currently active
    private bool touchActive = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        // Touch started, set touchActive to true
        touchActive = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Touch ended, set touchActive to false
        touchActive = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle touch click event
    }

    // Update is called once per frame
    void Update()
    {
        if (touchActive)
        {
            // Handle touch input
            TouchInput();
        }
    }

    void TouchInput()
    {
        // Get touch position
        Touch touch = Touchscreen.current.primaryTouch;

        if (touch.phase == TouchPhase.Began)
        {
            // Handle touch beginning
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            // Handle touch movement
        }
        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            // Handle touch end
        }
    }
}
*/