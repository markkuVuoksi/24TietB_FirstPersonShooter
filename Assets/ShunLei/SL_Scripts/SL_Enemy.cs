using UnityEngine;

public class SL_Enemy : MonoBehaviour, IDamageableSL
{
    public float maxHealth, health = 100.0f;
    
    // public float speed = 1f;           // Movement speed
    // public float distance = 5f;        // Distance to move from the starting position

    // public float rollSpeed = 200f;    // Rolling speed
    
    //private Rigidbody rb;
    private Vector3 startPosition;
    public ParticleSystem explosionParticle;

    public SL_HealthBar healthBar;

    public GameObject attackWarningMessage;

    void Start()
    {
        startPosition = transform.position;
        //rb = GetComponent<Rigidbody>();
        healthBar = GetComponentInChildren<SL_HealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);

        if (SL_EnemyManager.Instance != null)
        {
            SL_EnemyManager.Instance.RegisterEnemy();
        }
    }


    void Update()
    {
        //MoveEnemy();
    }

    private void OnDestroy()
    {
        if (SL_EnemyManager.Instance != null)
        {
            SL_EnemyManager.Instance.UnregisterEnemy();
        }
    }

    // public void MoveEnemy()
    // {
    //     // if (gameObject.name == "CircleEnemy")
    //     // {
    //     //     // Calculate position for left and right movement
    //     //     float offset = Mathf.PingPong(Time.time * speed, distance * 2) - distance;

    //     //     // Move the object left and right
    //     //     Vector3 targetPosition = startPosition + Vector3.right * offset;
    //     //     rb.MovePosition(targetPosition);

    //     //     // Roll the object as it moves
    //     //     rb.MoveRotation(rb.rotation * Quaternion.Euler(0, 0, -rollSpeed * Time.fixedDeltaTime * Mathf.Sign(offset)));
    //     // }

    //     // else
    //     // {
    //     // Move the object left and right using Mathf.PingPong
    //     float offset = Mathf.PingPong(Time.time * speed, distance * 2) - distance;
    //     transform.position = startPosition + Vector3.right * offset;
    //     //}

    // }

    public void TakeDamage(float damageAmount)

    {
        //Show particle effect when an object is hit
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

        health -= damageAmount;
        healthBar.UpdateHealthBar(health, maxHealth);

        //Debug.Log("Enemy health is: " + health);


        if (health <= 0)
        {
            Destroy(gameObject);

            //Check attack warning message is null or not because attack warning message is attched only with attack enemy (Grenadier) 
            if(attackWarningMessage != null)
            {
                attackWarningMessage.SetActive(false);
            }

        }

    }

}
