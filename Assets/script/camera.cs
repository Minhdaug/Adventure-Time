using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;  // Reference to the player's transform

    public float smoothSpeed = 0.125f;  // Adjust this to control the smoothness of the camera follow

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + new Vector3(0, 0, -10);  // Assuming camera is at Z = -10
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
