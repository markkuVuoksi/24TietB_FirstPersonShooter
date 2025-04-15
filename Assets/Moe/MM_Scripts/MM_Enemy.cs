using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MM_Enemy : MonoBehaviour, IDamageableMM
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float health;
    private Vector3 direction;
    private float speed = 50f;
    private Rigidbody rb;

    public Image healthBoarder;
    public Image healthBar;

    // Define the boundaries (min and max values for each axis)
    private float minX = -20f, maxX = 20f;
    private float minY = -1f, maxY = 10f;
    private float minZ = -40f, maxZ = 0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RandomizeHealth();

        //StartCo.. can repeat things 
        StartCoroutine(ChangeDirectionOverTime());
    }


    public void TakeDamage(float damageAmount)

    {

        health -= damageAmount;

        healthBar.fillAmount = health / 100f;

        Debug.Log("Enemy health is: " + health);

        if (health <= 0)

        {

            Destroy(gameObject);

        }

    }

    //To wait for a while
    private IEnumerator ChangeDirectionOverTime()
    {
        RandomizeDirection();

        while (true)
        {
            yield return new WaitForSeconds(3f);

            RandomizeDirection() ;
        }
    }
    
    void Move()
    {
        if (rb != null)
        {
            Vector3 enemyRotation = rb.transform.eulerAngles;
            enemyRotation.z = 0;
        }

       
        rb.MovePosition(transform.position+direction * speed * Time.deltaTime);
        RestrictOutOfBound();
    }
    void RandomizeDirection()
    {
        direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    //EXTRACTED FROM CHATGPT
    void RestrictOutOfBound()
    {
        // Get the current position of the enemy
        Vector3 position = transform.position;

        // Clamp each axis individually to stay within the boundaries
        position.x = Mathf.Clamp(position.x, minX, maxX);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        position.z = Mathf.Clamp(position.z, minZ, maxZ);

        // Set the new position, making sure it stays within the bounds
        transform.position = position;
    }
    void RandomizeHealth()
    {
        health = Random.Range(80f, 140f);
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
