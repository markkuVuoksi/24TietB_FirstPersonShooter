using UnityEngine;
using System.Collections;

public class AZ_Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float blastRadius = 5f;
    public float explosionForce = 700f;
    public float damageAmount = 50f;
    public LayerMask damageableLayer;

    public GameObject explosionEffectPrefab; // 🎇 Визуальный эффект
    public AudioClip explosionSound;         // 🔊 Звук взрыва
    private AudioSource audioSource;

    private bool hasExploded = false;

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

        // Воспроизводим звук
        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }

        // Эффект взрыва
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(explosionEffectPrefab, 3f);
        }

        // Найти все объекты в радиусе взрыва
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

        // Уничтожаем гранату после небольшой задержки (чтобы звук успел сыграть)
        Destroy(gameObject, 0.5f);
    }
}
