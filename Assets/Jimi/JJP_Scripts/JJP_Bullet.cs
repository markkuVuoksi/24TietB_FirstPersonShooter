using UnityEngine;

public class JJP_Bullet : MonoBehaviour
{
    public float damageJimi = 25.0f;
    public float lifetime = 5f;
    public GameObject enemyPrefab;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Use tag comparison
        {
            IDamageable_Jimi damageable = collision.transform.GetComponent<IDamageable_Jimi>();

            if (damageable != null)
            {
                damageable.TakeDamageJimi(damageJimi);
            }

            Destroy(gameObject); // Destroy bullet after hitting
        }
    }
}
