using UnityEngine;

public class MiniMapPlayer : MonoBehaviour
{
    public Transform followTransform; // Reference to the player's transform
    public float smoothSpeed = 5f; // Smoothing factor for the movement

    void Update()
    {
        // Ensure that playerTransform and miniMapIcon are assigned before proceeding
        if (followTransform)
        {
            // Calculate the target position with a slight offset on the y-axis
            Vector3 targetPosition = followTransform.position + Vector3.up * 40f;

            // Smoothly move the mini-map icon towards the target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}
