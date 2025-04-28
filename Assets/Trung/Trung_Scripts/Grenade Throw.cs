using System.Collections;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Camera playerCamera;
    public float throwForce = 10f;
    public AudioClip destroySound;
    public GameObject explosionEffect;
    public float explosionDamage = 50f;
    public float explosionRadius = 5f;
    public LayerMask explosionLayer;

    private float nextTimeToThrow = 0f; // Thời điểm được ném tiếp theo
    private float throwCooldown = 5f;   // Thời gian chờ giữa 2 lần ném

    private void Awake()
    {
        if (!playerCamera)
        {
            Debug.LogError("Assign a Camera for the script in the inspector");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && grenadePrefab != null && Time.time >= nextTimeToThrow)
        {
            nextTimeToThrow = Time.time + throwCooldown;
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.forward * 1.5f;
        GameObject grenade = Instantiate(grenadePrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);
        }

        StartCoroutine(PlayExplosion(grenade));
    }

    private IEnumerator PlayExplosion(GameObject grenade)
    {
        yield return new WaitForSeconds(3f);

        if (grenade != null)
        {
            Debug.Log("Grenade exploded at position: " + grenade.transform.position);

            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, grenade.transform.position, Quaternion.identity);
                Debug.Log("Explosion effect instantiated.");
            }

            if (destroySound != null)
            {
                AudioSource.PlayClipAtPoint(destroySound, grenade.transform.position);
                Debug.Log("Explosion sound played.");
            }

            Collider[] hitColliders = Physics.OverlapSphere(grenade.transform.position, explosionRadius, explosionLayer);
            foreach (Collider hitCollider in hitColliders)
            {
                IDamageable damageable = hitCollider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(explosionDamage);
                    Debug.Log("Damage dealt to: " + hitCollider.name);
                }
            }

            Destroy(grenade);
        }
    }
}
