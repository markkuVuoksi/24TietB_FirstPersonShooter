using UnityEngine;

public class SL_GrenadeThrow : MonoBehaviour
{
    public GameObject grenadePrefab;

    public Camera playerCamera;

    public float throwForce = 10f;


    public AudioSource audioSoruce;

    private float cooldownTime = 3f; // Cooldown time in seconds after each throw
    private float timeSinceLastThrow = 10f; // Timer to track time since last throw



    private void Awake()

    {

        if (!playerCamera)

        {

            Debug.LogError("Assign a Camera for the script in the inspector");

        }

    }



    void Update()

    {
        // Update the time since the last throw
        timeSinceLastThrow += Time.deltaTime;

        if (Input.GetButtonDown("Fire2") && grenadePrefab != null)

        {
            // Check if enough time has passed (cooldown is over)
            if (timeSinceLastThrow >= cooldownTime)
            {
                //Play throwing sound
                audioSoruce.Play();

                ThrowGrenade();

                // Reset the timer to start the cooldown
                timeSinceLastThrow = 0f;
            }
            else
            {
                // Inform the player if they try to throw the ball before cooldown is over
                Debug.Log("You need to wait " + (cooldownTime - timeSinceLastThrow).ToString("F1") + " seconds before throwing Grenade again.");
            }



        }

    }

    void ThrowGrenade()

    {

        GameObject grenade = Instantiate(grenadePrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        if (rb != null)

        {

            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);

        }

    }
}
