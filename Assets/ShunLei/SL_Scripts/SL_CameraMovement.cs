using UnityEngine;

public class SL_CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;

    private Transform playerBody;

    private float xRotation = 0.0f;

    private SL_PauseManager pauseManager;
    private SL_EnemyManager enemyManager;

    private void Start()

    {
        playerBody = transform.parent;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; // lock the cursor in the center of the screen
        pauseManager = GameObject.Find("pause button").GetComponent<SL_PauseManager>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<SL_EnemyManager>();

    }

    private void Update()

    {
        RotateCamera();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Show the mouse cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Allow the cursor to move freely

            if(!enemyManager.isGameEnd)
                pauseManager.PauseGame();
        }

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
