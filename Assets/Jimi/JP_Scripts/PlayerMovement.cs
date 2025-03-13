using UnityEngine;
public interface IDamageable

{

    void TakeDamage(float damageAmount);

}
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;

    public float jumpHeight = 2.0f;

    public float groundDistance = 0.4f;

    public LayerMask groundLayer;

    public Transform groundCheck;



    private CharacterController characterController;

    private Transform cameraTransform;

    private Vector3 velocity;

    private bool isGrounded;

    private float gravity = -9.81f;

    public float mouseSensitivity = 100.0f;

    private Transform playerBody;

    private float xRotation = 0.0f;
    public float range = 100.0f;

    public float damage = 25.0f;

    public Camera fpsCamera;

    public LayerMask shootingLayer;

    private void Start()

    {
        playerBody = transform.parent;

        Cursor.lockState = CursorLockMode.Locked;

        characterController = GetComponent<CharacterController>();

        cameraTransform = Camera.main.transform;

        if (groundCheck == null)

        {

            Debug.LogError("Assign a GroundCheck object to the PlayerMovement script in the inspector.");

            this.enabled = false;

            return;

        }

    }



    private void Update()

    {
        RotateCamera();
        MovePlayer();
        if (Input.GetButtonDown("Fire1"))

        {

            Shoot();

        }
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
    void Shoot()

    {

        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, shootingLayer))

        {

            // Debuggausta, piirrellään drawray, kun osutaan kohteeseen

            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red, 2.0f);

            // checkaa jos objekti on sellainen mihin voi osua

            IDamageable damageable = hit.transform.GetComponent<IDamageable>();

            if (damageable != null)

            {

                damageable.TakeDamage(damage);

            }

        }
    }


    private void MovePlayer()

    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0)

        {

            velocity.y = -2f; // pidä hahmo maassa

        }



        float horizontal = Input.GetAxisRaw("Horizontal");

        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 forwardDirection = cameraTransform.forward;

        Vector3 rightDirection = cameraTransform.right;



        forwardDirection.y = 0; // niin ettei pelaaja lähde lentämään y akselilla.

        rightDirection.y = 0;

        forwardDirection.Normalize();

        rightDirection.Normalize();



        Vector3 desiredDirection = (forwardDirection * vertical + rightDirection * horizontal).normalized;

        Vector3 movement = desiredDirection * speed * Time.deltaTime;



        characterController.Move(movement);



        // hyppy logiikka

        if (Input.GetButtonDown("Jump") && isGrounded)



        {

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }



        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

    }

}
