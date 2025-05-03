using UnityEngine;

public class AZ_Enemy : MonoBehaviour, IDamageableAZ
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float health = 100.0f;
    //public float moveSpeed = 2f;
    //private Vector3 moveDirection = Vector3.forward;
    void Start()
    {
    }
    //void Update()
    //{
    //    transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    //}

    public void TakeDamage(float damageAmount)

    {

        health -= damageAmount;

        Debug.Log("Enemy health is: " + health);

        if (health <= 0)

        {

            Destroy(gameObject);

        }

    }

    private void OnDestroy()
    {
        if (AZ_GameManager.Instance != null)
        {
            AZ_GameManager.Instance.OnEnemyKilled();
        }
    }
}

