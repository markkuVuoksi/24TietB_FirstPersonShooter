using UnityEngine;

public class JP_Enemy : MonoBehaviour, IDamageable
{
    public float health = 100.0f;

    // Toteutus IDamageable-rajapinnan TakeDamage-metodille
    public void TakeDamage(float damageAmount)
    {
        // Varmistetaan, ettei vahinkomäärä ole negatiivinen
        if (damageAmount < 0)
        {
            Debug.LogWarning("Damage amount cannot be negative.");
            return;
        }

        health -= damageAmount;
        Debug.Log("Enemy health is: " + health);

        // Jos terveys menee nollaan tai alle, tuhotaan peliobjekti
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}