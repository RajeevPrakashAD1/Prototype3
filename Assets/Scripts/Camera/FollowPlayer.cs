using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform Player; // Reference to the player's transform
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Offset from the player's position
    public float smoothSpeed = 5f; // Smoothing speed of camera movement

    void Update()
    {
        if (Player != null)
        {
            // Calculate the desired position for the camera
            Vector3 desiredPosition = Player.position + offset;

            // Smoothly move the camera towards the desired position using linear interpolation
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
 