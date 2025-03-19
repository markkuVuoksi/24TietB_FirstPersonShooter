using UnityEngine;
using System.Collections;

public class JA_Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float blastRadius = 5f;
    public float explosionForce = 700f;
    public float damageAmount = 50f;
    public LayerMask damageableLayer;
    private bool hasExploded = false;
    public AudioClip soundEffect;
    private AudioSource audioSource;
    public ParticleSystem effect;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        TriggerEffect();
         
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
        hasExploded = true;
        Destroy(gameObject);
    }

    /*public void PlaySound()
    {
        audioSource.PlayOneShot(soundEffect);
    }

    public void PlayEffect()
    {
        effect.Play();
    }*/

    public void TriggerEffect()
    {
       audioSource.PlayOneShot(soundEffect);
       effect.Play();
    }
}
