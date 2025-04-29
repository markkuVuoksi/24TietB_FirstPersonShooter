using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class SL_AITarget : MonoBehaviour
{
    public Transform Target;
    public float AttackDistance;

    private NavMeshAgent m_Agent;
    private Animator m_Animator;
    private float m_Distance;

    private float playerMaxHealth = 100.0f;
    private float health = 100.0f;

    public GameObject hb;
    public GameObject attackMessage;
    private SL_HealthBar healthBar;

    private SL_EnemyManager enemyManager;
    private SL_PauseManager pauseManager;
    public float damageAmount = 0.07f;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        healthBar = hb.GetComponent<SL_HealthBar>();
        healthBar.UpdateHealthBar(health, playerMaxHealth);
        enemyManager = GameObject.Find("EnemyManager").GetComponent<SL_EnemyManager>();
        //pauseManager = GameObject.Find("pause button").GetComponent<SL_PauseManager>();
        attackMessage.SetActive(false);
    }

    void Update()
    {
        m_Distance = Vector3.Distance(m_Agent.transform.position, Target.position);
        if (m_Distance < AttackDistance && !enemyManager.isGameEnd)
        {
            m_Agent.isStopped = true;
            m_Animator.SetBool("Attack", true);

            health = health - damageAmount;
            healthBar.UpdateHealthBar(health, playerMaxHealth);
            attackMessage.SetActive(true);
        }
        else
        {
            m_Agent.isStopped = false;
            m_Animator.SetBool("Attack", false);
            m_Agent.destination = Target.position;
            attackMessage.SetActive(false);
        }

        if (health < 0 && !enemyManager.isGameEnd)
        {
            // Show the mouse cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Allow the cursor to move freely

            enemyManager.isGameEnd = true;
            enemyManager.GameOverDead();
            attackMessage.SetActive(false);
        }

    }

    void OnAnimatorMove()
    {
         if (Time.deltaTime <= 0f) return; // Prevent division by zero during pause

        if (m_Animator.GetBool("Attack") == false)
        {
            m_Agent.speed = (m_Animator.deltaPosition / Time.deltaTime).magnitude;
            attackMessage.SetActive(false);
        }
    }

}
