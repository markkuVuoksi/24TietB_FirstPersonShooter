using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class MM_Grenadier : MonoBehaviour
{
    public float health = 100f;
    public Image healthBoarder;
    public Image healthBar;
    private Camera _cam;

    public Transform Target;
    public float AttackDistance;
    private float HealthReduction = 1.4f;

    public ParticleSystem explode;

    //to make that ReduceHealth() wont occur frame per second
    private bool isAttacking = false;
    public bool stopAnimation = false;

    private UnityEngine.AI.NavMeshAgent m_Agent;
    private float m_Distance;

    public MM_PlayerMovement playerMovement;
    public Animator m_Animator;
    public MM_Enemy _Enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = Object.FindAnyObjectByType<MM_PlayerMovement>();
        _Enemy = Object.FindAnyObjectByType<MM_Enemy>();
        _cam = Camera.main;

        m_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_Animator.applyRootMotion = false;

        m_Agent.isStopped = true;
    }

    // Update is called once per frame
    void Update()
    {
        //follow the player
        m_Distance = Vector3.Distance(m_Agent.transform.position, Target.position);

        if(m_Distance < AttackDistance && !isAttacking && !playerMovement.GameOver && !_Enemy.YouWin)
        {
            m_Agent.isStopped = true;
            m_Animator.SetBool("Attack", true);

            if(!isAttacking)
            {
                StartCoroutine(ReduceHealth());
            }
        }
        else if (m_Distance > AttackDistance && m_Agent.isStopped && !playerMovement.GameOver && !_Enemy.YouWin) 
        {
            m_Agent.isStopped = false;
            m_Animator.SetBool("Attack", false);
        }
        else if(m_Distance > AttackDistance)
        {
            m_Agent.destination = Target.position;
        }
        //make health bar look at camera
        healthBoarder.transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        healthBar.transform.rotation = healthBoarder.transform.rotation;

        StartCoroutine(GrenadierDeath(6f));
    }

    private void OnAnimatorMove()
    {
        if(m_Animator.GetBool("Attack") == false)
        {
            m_Agent.speed = (m_Animator.deltaPosition / Time.deltaTime).magnitude;
        }

        //update the health bar
        healthBar.fillAmount = health / 100;
    }

    IEnumerator ReduceHealth()
    {
        if(playerMovement.playerHealth <= 0 && !playerMovement.isDead )
        {
            playerMovement.isDead = true;
            playerMovement.LostMenu();
        }

        isAttacking = true;
        yield return new WaitForSeconds(HealthReduction);

        //Reduce player health
        if (playerMovement.playerHealth > 0)
        {
            playerMovement.playerHealth -= Random.Range(20,30);
        }
        isAttacking = false;
    }

    public void GrenadierStopForASecond()
    {
        Debug.Log("Grenadier has stopped");
        StartCoroutine(StunGre(2f));
    }

    IEnumerator StunGre(float duration)
    {
        //Stun the enemy
        m_Agent.isStopped = true;
        m_Animator.SetBool("Stunned", true);
        isAttacking = true;

        yield return new WaitForSeconds(duration);

        //Resume walking
        m_Agent.isStopped = false;
        isAttacking = false;
        m_Animator.SetBool("Stunned", false);
    }

    IEnumerator GrenadierDeath(float duration)
    {
        if(health == 0)
        {
            StartCoroutine(Animation());
            m_Agent.isStopped = true;
            m_Animator.SetBool("Die", true);
            yield return new WaitForSeconds(duration);
            Destroy(gameObject);
        }
        
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(2);
        explode.Play();
    }

}
