using UnityEngine;

public class CrossHair : MonoBehaviour
{
    private Camera mainCamera;
    public float distance = 10f; // Distance of crosshair from camera

    void Start()
    {
        // Find the main camera in the scene
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Ensure the main camera is found
        if (mainCamera == null)
            return;

        // Position the crosshair in front of the camera
        transform.position = mainCamera.transform.position + mainCamera.transform.forward * distance;

        // Make the crosshair always face the camera
        transform.LookAt(mainCamera.transform);
    }
}
