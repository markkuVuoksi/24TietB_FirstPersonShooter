using UnityEngine;

public class JJP_Bullet : MonoBehaviour
{
    public float damageJimi = 25.0f;  // The damage the bullet does when it hits an enemy
    public float lifetime = 5f;       // Lifetime of the bullet before it gets destroyed (in case it doesn't hit anything)
    public GameObject enemyPrefab;    // Reference to the enemy prefab that the bullet can damage
    
    private void Start()
    {
        // Destroy the bullet after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet hits an object that is damageable and is the assigned enemy prefab
        if (collision.gameObject == enemyPrefab)
        {
            // Attempt to get the damageable component from the enemy
            IDamageable_Jimi damageable = collision.transform.GetComponent<IDamageable_Jimi>();

            if (damageable != null)
            {
                // Apply damage to the object
                damageable.TakeDamageJimi(damageJimi);
            }

            // Destroy the bullet after it collides with something
            Destroy(gameObject);
        }
    }
}
