using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JL_Enemy : MonoBehaviour, IDamageable
{
    public float health = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
