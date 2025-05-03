using UnityEngine;
using System.Collections;


public class AZ_Grenade : AZ_BaseGrenade
{
    public float blastRadius = 5f;
    public float explosionForce = 700f;
    public float damageAmount = 50f;
    public LayerMask damageableLayer;
    public GameObject explosionEffectPrefab;
    public AudioClip explosionSound;

    private AudioSource audioSource;
    private bool hasExploded = false;

    protected override void Start()
    {
        audioSource = GetComponent<AudioSource>();
        base.Start();
    }

    protected override void Explode()
    {
        if (hasExploded) return;

        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }

        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius, damageableLayer);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }

            IDamageableAZ damageable = nearbyObject.GetComponent<IDamageableAZ>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
            }
        }

        hasExploded = true;
        Destroy(gameObject, 0.5f);
    }
}
