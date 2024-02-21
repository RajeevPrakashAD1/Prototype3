using UnityEngine;

public class MiniMapPlayer : MonoBehaviour
{
    public Transform followTransform; // Reference to the player's transform
   

    void Update()
    {
        // Ensure that playerTransform and miniMapIcon are assigned before proceeding
        if (followTransform)
        {
            // Set the position of the mini-map icon to match the player's position
            transform.position = new Vector3(followTransform.position.x ,20f , followTransform.position.z);

            // Set the rotation of the mini-map icon to match the player's rotation
            transform.rotation = Quaternion.Euler(0f, followTransform.eulerAngles.y, 0f);
        }
    }
}
