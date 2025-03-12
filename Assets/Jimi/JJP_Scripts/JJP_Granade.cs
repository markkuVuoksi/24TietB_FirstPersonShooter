using UnityEngine;
using System.Collections;

public class JJP_Granade : MonoBehaviour
{
    public float delay = 3f;                   // Delay before explosion
    public float blastRadius = 5f;             // Radius of the explosion
    public float explosionForce = 700f;        // Force applied to nearby objects
    public float damageAmount = 50f;           // Damage dealt to nearby damageable objects
    public LayerMask damageableLayer;          // Layer mask for detecting damageable objects

    private bool hasExploded = false;          // To check if the grenade has exploded

    void Start()
    {
        // Start the explosion after the delay
        StartCoroutine(TriggerExplosionAfterDelay());
    }

    // Coroutine to handle the delayed explosion
    IEnumerator TriggerExplosionAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    // The actual explosion logic
    void Explode()
    {
        if (hasExploded) return; // Prevent explosion multiple times

        // Get all colliders within the blast radius that are on the damageable layer
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius, damageableLayer);

        foreach (Collider nearbyObject in colliders)
        {
            // Apply explosion force to rigidbodies within blast radius
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculate distance for more realistic force falloff
                float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);
                float force = Mathf.Lerp(explosionForce, 0f, distance / blastRadius);  // Reduce force with distance
                rb.AddExplosionForce(force, transform.position, blastRadius);
            }

            // Apply damage to damageable objects
            IDamageable damageable = nearbyObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
            }
        }

        // Mark the grenade as exploded and destroy it
        hasExploded = true;
        Destroy(gameObject);
    }

    // Update method is not needed, so we can remove it
    //void Update()
    //{
    //}
}
