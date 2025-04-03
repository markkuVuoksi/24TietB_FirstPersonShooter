using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JL_Enemy : MonoBehaviour, IDamageable
{
    public float health = 100.0f;
    public float speed = 3.0f;
    private Transform target;
    private float yMin = -1f; // Nếu rơi dưới yMin, enemy sẽ chết ngay
    private EnemySpawner spawner;
    KillCount KillCount;
    public AudioClip deathSound;
    AudioSource enmyAudio;


    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        spawner = FindObjectOfType<EnemySpawner>();
        KillCount = GameObject.Find("Print Kill Count").GetComponent<KillCount>();
        enmyAudio = GetComponent<AudioSource>();


    }

    private void Update()
    {
        ChaseTarget();

        // Nếu enemy rơi xuống vực, nó sẽ chết ngay
        if (transform.position.y < yMin)
        {
            Die();
            enmyAudio.PlayOneShot(deathSound);
            KillCount.IncrementKillCount();

        }
    }

    private void ChaseTarget()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.LookAt(target);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        Debug.Log("Enemy health: " + health);

        if (health <= 0)
        {
            Die();
            KillCount.IncrementKillCount();
            enmyAudio.PlayOneShot(deathSound);
        }
    }

    private void Die()
    {
        if (spawner != null)
        {
            spawner.EnemyDied(); // Gọi Spawner để báo enemy đã chết

        }

        Destroy(gameObject);
        Debug.Log("Enemy died");
    }
}


