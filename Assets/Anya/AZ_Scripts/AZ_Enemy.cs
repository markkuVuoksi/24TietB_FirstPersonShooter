using UnityEngine;

public class AZ_Enemy : MonoBehaviour, IDamageableAZ
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float health = 100.0f;
    void Start()
    {
    }
     
    public void TakeDamage(float damageAmount)

    {

        health -= damageAmount;

        Debug.Log("Enemy health is: " + health);

        if (health <= 0)

        {

            Destroy(gameObject);

        }

    }


}

