using UnityEngine;

public class JJP_Granadethrow : MonoBehaviour
{
    public GameObject grenadePrefab;            // Reference to the grenade prefab
    public Camera playerCamera;                 // Reference to the player's camera
    public float throwForce = 10f;              // Force applied when throwing the grenade
    public float throwCooldown = 3f;            // Cooldown time between throws (adjustable in the Inspector)

    private float lastThrowTime = 0f;           // Time of the last grenade throw

    private void Awake()
    {
        if (!playerCamera)
        {
            Debug.LogError("Assign a Camera for the script in the inspector");
        }
    }

    void Start()
    {
        lastThrowTime = -throwCooldown;  // Allow the first throw immediately after the game starts
    }

    void ThrowGrenadeJimi()
    {
        // Instantiate the grenade at the camera's position plus forward direction
        GameObject grenade = Instantiate(grenadePrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Apply the throw force in the direction the camera is facing
            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);
        }
    }

    void Update()
    {
        // Check if the player pressed the button to throw a grenade and if the cooldown has passed
        if (Input.GetButtonDown("Fire2") && grenadePrefab != null && Time.time >= lastThrowTime + throwCooldown)
        {
            // Throw the grenade
            ThrowGrenadeJimi();

            // Update the last throw time
            lastThrowTime = Time.time;
        }
    }
}
