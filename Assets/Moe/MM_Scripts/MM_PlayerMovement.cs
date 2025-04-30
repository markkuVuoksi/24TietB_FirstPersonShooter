using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MM_PlayerMovement : MonoBehaviour
{
    //Player HealthBar
    public float playerHealth = 100.0f;
    public Image healthBoarder;
    public Image healthBar;

    public float speed = 5.0f;

    public float jumpHeight = 2.0f;

    public float groundDistance = 0.4f;

    public LayerMask groundLayer;

    public Transform groundCheck;

    //related with lost UI
    public GameObject gameOverUI;
    public bool isDead = false;


    private CharacterController characterController;

    private Transform cameraTransform;

    private Vector3 velocity;

    private bool isGrounded;

    private float gravity = -9.81f; // you may need to adjust this to simulate physics



    private void Start()

    {

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

        MovePlayer();

        //update the health bar
        healthBar.fillAmount = playerHealth / 100;

    }

    public void LostMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        gameOverUI.SetActive(true);
    }



    private void MovePlayer()

    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (isGrounded && velocity.y < 0)

        {

            velocity.y = -2f; // keep the character on the ground

        }



        float horizontal = Input.GetAxisRaw("Horizontal");

        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 forwardDirection = cameraTransform.forward;

        Vector3 rightDirection = cameraTransform.right;



        forwardDirection.y = 0; // so that the player doesn't fly on the y axis.

        rightDirection.y = 0;

        forwardDirection.Normalize();

        rightDirection.Normalize();



        Vector3 desiredDirection = (forwardDirection * vertical + rightDirection * horizontal).normalized;

        Vector3 movement = desiredDirection * speed * Time.deltaTime;



        characterController.Move(movement);



        // jump logic

        if (Input.GetButtonDown("Jump") && isGrounded)



        {

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }



        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

    }

}




