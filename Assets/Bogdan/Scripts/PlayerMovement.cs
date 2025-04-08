using NUnit.Framework;
using UnityEngine;


namespace BogdanVerkhovod
{
    public class PlayerMovement : MonoBehaviour
    {

        public float speed = 5.0f;

        private CharacterController characterController;

        private Transform cameraTransform;

        private void Start()

        {

            characterController = GetComponent<CharacterController>();

            cameraTransform = Camera.main.transform;

        }



        private void Update()

        {

            MovePlayer();

        }



        private void MovePlayer()

        {

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
}
