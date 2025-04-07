using System.Collections;
using UnityEngine;

public class Aleksandr_Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float blastRadius = 5f;
    public float explosionForce = 700f;
    public float damageAmount = 50f;
    public LayerMask damageableLayer;

    public AudioClip explosionSound; 
    private bool hasExploded = false;

    void Start()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    void Explode()
    {
        if (hasExploded) return;


        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius, damageableLayer);
        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }

            IDamageable damageable = nearbyObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
            }
        }


        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        hasExploded = true;


        Destroy(gameObject);
    }
}
