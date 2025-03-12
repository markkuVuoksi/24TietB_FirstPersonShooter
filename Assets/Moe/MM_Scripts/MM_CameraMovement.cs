using UnityEngine;

public class MM_CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;

    private Transform playerBody;

    private float xRotation = 0.0f;

    private void Start()

    {

        playerBody = transform.parent;

        Cursor.lockState = CursorLockMode.Locked; // lock the cursor in the center of the screen

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

        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f); // so the camera doesn't rotate upside down

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        playerBody.Rotate(Vector3.up * mouseX);

    }
}
