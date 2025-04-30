using UnityEngine;
using System.Collections;

public class JA_Grenade : MonoBehaviour
{
    [Header("Timing & Physics")]
    public float delay = 3f;
    public float blastRadius = 5f;
    public float explosionForce = 700f;

    [Header("Damage")]
    public float damageAmount = 50f;
    public LayerMask damageableLayer;

    [Header("Effects")]
    public AudioClip explosionClip; // ääni
    public ParticleSystem explosionEffect; // partikkeli-prefab

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
        hasExploded = true;
         
        // Soitetaan ääni maailman pisteessä
        if (explosionClip != null)
            AudioSource.PlayClipAtPoint(explosionClip, transform.position);

        // Instansioidaan partikkeli-prefab ja tuhotaan se kun animaatio loppuu
        if (explosionEffect != null)
        {
            ParticleSystem ps = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            ps.Play();
            Destroy(ps.gameObject, ps.main.duration);
        }

        // Fysiikka & damage
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius, damageableLayer);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);

            IDamageable_JA dmg = hit.GetComponent<IDamageable_JA>();

            if (dmg != null)
                dmg.TakeDamage(damageAmount);
        }

        Destroy(gameObject);
    }
}
