using UnityEngine;

public class JJP_CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;
    private float xRotation = 0.0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Lock cursor to center
        Cursor.visible = false;  // Hide cursor
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
