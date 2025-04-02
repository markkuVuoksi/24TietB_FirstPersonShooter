using UnityEngine;
using System.Collections;

public class JJP_Granade : MonoBehaviour
{
    public float delay = 3f;                   // Delay before explosion
    public float blastRadius = 5f;             // Radius of the explosion
    public float explosionForce = 700f;        // Force applied to nearby objects
    public float damageAmount = 50f;           // Damage dealt to nearby damageable objects
    public LayerMask damageableLayer;          // Layer mask for detecting damageable objects

    public ParticleSystem explosionEffect;     // Particle effect for explosion

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

        // Instantiate explosion effect and play it
        if (explosionEffect != null)
        {
            ParticleSystem effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration); // Destroy particle system after its effect ends
        }

        // Apply explosion force and damage
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius, damageableLayer);

        foreach (Collider nearbyObject in colliders)
        {
            // Apply explosion force
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                float distance = Vector3.Distance(transform.position, nearbyObject.transform.position);
                float force = Mathf.Lerp(explosionForce, 0f, distance / blastRadius); // Reduce force with distance
                rb.AddExplosionForce(force, transform.position, blastRadius);
            }

            // Apply damage
            IDamageable damageable = nearbyObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
            }
        }

        hasExploded = true;
        Destroy(gameObject); // Destroy the grenade after explosion
    }
}
