using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController characterController;
    private Transform cameraTransform;


    public float jumpHeight = 2f;
    public float groundDistance = 0.4f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private Vector3 velocity;
    private bool isGrounded;
    private float gravity = -9.8f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        if (groundCheck == null )
        {
            Debug.LogError("Assign a GroundCheck object to the PlayerMovement script in the inspector.");
            this.enabled = false;
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {   
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        if (isGrounded && velocity.y < 0)

        {

            velocity.y = -2f; 

        }

        if (Input.GetButtonDown("Jump") && isGrounded)

        {

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 forwardDirection = cameraTransform.forward;
        Vector3 rightDirection = cameraTransform.right;

        forwardDirection.y = 0;
        rightDirection.y = 0;

        forwardDirection.Normalize();
        rightDirection.Normalize();

        Vector3 desiredDirection = (forwardDirection * vertical + rightDirection * horizontal).normalized;
        Vector3 movement = desiredDirection * speed * Time.deltaTime;

        characterController.Move(movement);
    }
}
