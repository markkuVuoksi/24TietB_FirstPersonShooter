using UnityEngine;

public class Aleksandr_GrenadeThrow : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Camera playerCamera;
    public float throwForce = 10f;
    public float grenadeDamage = 50f; // Damage dealt by the grenade

    private void Awake()
    {
        if (!playerCamera)
        {
            playerCamera = Camera.main;
            if (!playerCamera)
            {
                Debug.LogError("Assign a Camera for the script in the inspector");
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && grenadePrefab != null)
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player Camera is not assigned.");
            return;
        }

        GameObject branched = Instantiate(grenadePrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);
        Rigidbody rb = branched.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);
        }

        // Add a script to handle the grenade's explosion and damage
        Grenade grenadeScript = branched.AddComponent<Grenade>();
        grenadeScript.damage = grenadeDamage;
    }
}

public class Grenade : MonoBehaviour
{
    public float damage;

    private void OnCollisionEnter(Collision collision)
    {
        // Assuming the target has a health component
        Health targetHealth = collision.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
        }

        // Destroy the grenade after collision
        Destroy(gameObject);
    }
}

public class Health : MonoBehaviour
{
    public float currentHealth = 100f;

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle the object's death
        Destroy(gameObject);
    }
}
