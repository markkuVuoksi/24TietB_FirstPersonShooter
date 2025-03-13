using UnityEngine;

public class JA_Enemy : MonoBehaviour, IDamageable_JA
{
    public float health = 100.0f;
    public void TakeDamage(float damageAmount)
    {
        Debug.Log("Take damage");
        health -= damageAmount;
        Debug.Log("Enemy health is: " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
}
