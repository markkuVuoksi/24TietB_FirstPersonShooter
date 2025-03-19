using UnityEngine;

public class OS_Camera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     public float mouseSensitivity = 100.0f;
    private Transform playerBody;
    private float xRotation = 0.0f;
    void Start()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked; // lukitse kursori keskelle ruutua
    }

    // Update is called once per frame
    void Update()
    {
         RotateCamera();
    }
     private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f); // niin että kamera ei pyöri ylösalaisin
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
