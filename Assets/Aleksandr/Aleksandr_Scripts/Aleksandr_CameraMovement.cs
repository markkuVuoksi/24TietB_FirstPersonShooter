using NUnit.Framework;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.Device;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class Aleksandr_CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float mouseSensitivity = 100.0f;

    private Transform playerBody;

    private float xRotation = 0.0f;

    private void Start()

    {

        playerBody = transform.parent;

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

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
