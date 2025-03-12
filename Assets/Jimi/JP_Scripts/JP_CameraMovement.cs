using UnityEngine;

public class JP_CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;
    private float xRotation = 0.0f;

    private void Start()
    {
        // Lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Clamp the vertical rotation to prevent camera flip
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        // Apply rotation to the camera (vertical rotation)
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        // Apply horizontal rotation to the player body
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
