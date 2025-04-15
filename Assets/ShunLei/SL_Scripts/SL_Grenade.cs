using UnityEngine;
using System.Collections;
using Unity.Collections;

public class SL_Grenade : MonoBehaviour
{
    public float delay = 2f;

    public float blastRadius = 5f;

    public float explosionForce = 700f;

    public float damageAmount = 50f;

    public LayerMask damageableLayer;

    public ParticleSystem explosionParticle;

    private bool hasExploded = false;

    public AudioClip explosionSound; // The sound to play when the object explodes
    private AudioSource audioSource; // AudioSource to play sound


    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // Ensure an AudioClip is assigned (can be assigned through inspector)
        if (audioSource != null)
        {
            audioSource.clip = explosionSound;
        }
        else
        {
            Debug.LogError("No AudioSource found on this GameObject!");
        }

        StartCoroutine(ExplodeAfterDelay());
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    IEnumerator PlaySound()
    {
        if (audioSource != null)
        {
            // Check if audio is playing
            Debug.Log("Audio Clip: " + audioSource.clip.name);

            audioSource.Play();  // Play the AudioSource when collision occurs
            while (audioSource.isPlaying)
            {
                //Debug.Log("Is Playing: " + audioSource.isPlaying);
                yield return null;
            }
        }
        else
        {
            Debug.LogWarning("No AudioSource component found on this GameObject.");
        }
    }

    void Explode()
    {
        
        if (hasExploded) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius, damageableLayer);

        foreach (Collider nearbyObject in colliders)
        {
            //Debug.Log("Near object name" + nearbyObject.name);
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }

            IDamageableSL damageable = nearbyObject.GetComponent<IDamageableSL>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
            }
        }

        hasExploded = true;

        // Start the PlaySound coroutine and wait for it to finish before destroying the game object
        StartCoroutine(PlaySoundAndDestroy());
    }

    IEnumerator PlaySoundAndDestroy()
    {
        yield return StartCoroutine(PlaySound());

        // Show particle effect when an object is exploded
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

        Destroy(gameObject);
       
    }
}
