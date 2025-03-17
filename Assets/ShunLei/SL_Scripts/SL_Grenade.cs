using UnityEngine;
using System.Collections;

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



    void Explode()
    {

        //Show particle effect when an object is exploded
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

        if (audioSource != null)
        {
            // Check if audio is playing
            Debug.Log("Audio Clip: " + audioSource.clip.name);

            audioSource.Play();  // Play the AudioSource when collision occurs

            Debug.Log("Is Playing: " + audioSource.isPlaying);
        }
        else
        {
            Debug.LogWarning("No AudioSource component found on this GameObject.");
        }

        if (hasExploded) return;



        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius, damageableLayer);

        foreach (Collider nearbyObject in colliders)

        {

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

        Destroy(gameObject);

    }
}
