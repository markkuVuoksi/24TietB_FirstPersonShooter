using UnityEngine;




public class JP_PlayerMovement : MonoBehaviour
{
    // Player movement variables
    public float speed = 5.0f;
    public float jumpHeight = 2.0f;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    // Player movement control variables
    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;
    private float gravity = -9.81f;
    public Camera fpsCamera;

    private void Start()
    {
        // Initialize variables
        characterController = GetComponent<CharacterController>();

        if (groundCheck == null)
        {
            Debug.LogError("Assign a GroundCheck object to the PlayerMovement script in the inspector.");
            this.enabled = false;
            return;
        }
    }

    private void MovePlayer()
    {
        // Check if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        // If grounded, reset vertical velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // keep player grounded
        }

        // Movement input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Calculate movement direction based on camera
        Vector3 forwardDirection = fpsCamera.transform.forward;
        Vector3 rightDirection = fpsCamera.transform.right;

        // Remove vertical component from the directions
        forwardDirection.y = 0;
        rightDirection.y = 0;

        forwardDirection.Normalize();
        rightDirection.Normalize();

        // Calculate desired movement direction
        Vector3 desiredDirection = (forwardDirection * vertical + rightDirection * horizontal).normalized;
        Vector3 movement = desiredDirection * speed * Time.deltaTime;

        // Move the player
        characterController.Move(movement);

        // Jumping logic
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Set jump force
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Update()
    {
        // Call movement method
        MovePlayer();
    }
}
