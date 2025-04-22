using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class MM_Grenadier : MonoBehaviour
{
    public float health = 100f;
    public Image healthBoarder;
    public Image healthBar;
    private Camera _cam;

    public Transform Target;
    public float AttackDistance;

    private UnityEngine.AI.NavMeshAgent m_Agent;
    private Animator m_Animator;
    private float m_Distance;

    public MM_PlayerMovement playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = Object.FindAnyObjectByType<MM_PlayerMovement>();

        m_Agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_Animator.applyRootMotion = false;
    }

    // Update is called once per frame
    void Update()
    {
        //follow the player
        m_Distance = Vector3.Distance(m_Agent.transform.position, Target.position);

        if(m_Distance < AttackDistance )
        {
            m_Agent.isStopped = true;
            m_Animator.SetBool("Attack", true);
            playerMovement.playerHealth -= Random.Range(10, 20);
        }
        else if (m_Distance > AttackDistance ) 
        {
            m_Agent.isStopped = false;
            m_Animator.SetBool("Attack", false);
            m_Agent.destination = Target.position;
        }
        //make health bar look at camera
        healthBoarder.transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
        healthBar.transform.rotation = healthBoarder.transform.rotation;
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
}
