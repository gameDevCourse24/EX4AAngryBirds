using UnityEngine;
using System.Collections;

// This script follows the bird on the X Axis.
public class CameraFollower : MonoBehaviour
{
    // The object you want to follow after
    [SerializeField, Tooltip("The object you want to follow after")]
    public GameObject PlayerToFollowXAxis;

    // The regular size for the camera
    [SerializeField, Tooltip("The regular size for the camera")]
    float regularSizeForCamera = 12;

    // The speed of the camera when it shows the level
    [SerializeField, Tooltip("The speed of the camera when she shows the level")]
    float cameraSpeed = 2f;

    // Target X position for the camera to follow
    float targetX;

    // Initialize the camera settings
    void start()
    {
        // Set the target X position to the player's current position
        targetX = PlayerToFollowXAxis.transform.position.x;

        // Get the Camera component and set its orthographic size
        Camera cameraComponent = GetComponent<Camera>();
        if (cameraComponent != null)
        {
            cameraComponent.orthographicSize = regularSizeForCamera; // Set the camera size
        }
    }

    // Resize the camera based on a scale multiplier
    public void resizeCamera(float scaleMultuply)
    {
        Camera.main.orthographicSize = regularSizeForCamera * scaleMultuply; // Update the camera size
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has moved past the target X position
        if (PlayerToFollowXAxis.transform.position.x >= targetX && PlayerToFollowXAxis != null)
        {
            // Create a new position for the camera based on the player's X position
            Vector3 newPosition = new Vector3(PlayerToFollowXAxis.transform.position.x, transform.position.y, transform.position.z);
            transform.position = newPosition; // Update the camera's position
        }
    }

    // Move the camera from startPoint to endPoint
    public void MoveCamera(Vector3 startPoint, Vector3 endPoint)
    {
        StartCoroutine(MoveCameraCoroutine(endPoint, startPoint)); // Start the coroutine to move the camera
    }

    // Coroutine to smoothly move the camera from the startPoint to the endPoint
    private IEnumerator MoveCameraCoroutine(Vector3 startPoint, Vector3 endPoint)
    {
        // Calculate the duration based on the distance and camera speed
        float duration = Vector3.Distance(startPoint, endPoint) / cameraSpeed;
        float elapsedTime = 0f;

        // Smoothly interpolate the camera's position over the duration
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPoint, endPoint, (elapsedTime / duration)); // Interpolate position
            elapsedTime += Time.deltaTime;// Increment elapsed time
            yield return null;// Wait for the next frame
        }
        // Ensure the camera ends at the exact endPoint
        transform.position = endPoint;
    }
}