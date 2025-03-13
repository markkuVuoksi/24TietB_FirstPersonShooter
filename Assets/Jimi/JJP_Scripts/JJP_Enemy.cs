using UnityEngine;

public class JJP_Enemy : MonoBehaviour, IDamageable_Jimi
{
    public float healthJimi = 100.0f;

    // Implement the TakeDamage method from the IDamageable interface
    public void TakeDamageJimi(float damageAmount)
    {
        if (damageAmount < 0)
        {
            Debug.LogWarning("Damage amount cannot be negative.");
            return;
        }

        healthJimi -= damageAmount;
        Debug.Log("Enemy health is: " + healthJimi);

        if (healthJimi <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Enemy destroyed!");
        }
    }
}
