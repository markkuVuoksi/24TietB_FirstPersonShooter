using UnityEngine;
using UnityEngine.AI;

public class JA_Enemy_AI : MonoBehaviour
{
    [Header("General")]
    public Transform player;
    public LayerMask sightLayers;

    [Header("Distances (metreinä)")]
    public float sightRadius = 20f;
    public float chaseRadius = 25f;
    public float attackRadius = 8f;

    [Header("Patrol")]
    public Transform patrolParent;
    public float patrolWaitTime = 2f;

    [Header("Weapon")]
    public float fireRate = 1f;
    public float bulletDamage = 20f;
    public AudioClip shotClip;

    enum State { Patrol, Chase, Attack }
    State state = State.Patrol;

    private NavMeshAgent agent;
    private Transform[] patrolPoints;
    private int index;
    private float waitTimer;
    private float fireTimer;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p) player = p.transform;
        }

        if (patrolParent)
        {
            int n = patrolParent.childCount;
            patrolPoints = new Transform[n];
            for (int i = 0; i < n; i++)
                patrolPoints[i] = patrolParent.GetChild(i);

            index = 0;
            if (n > 0)
            agent.SetDestination(patrolPoints[0].position);
        }
    }

    void Update()
    {
        if (player == null) return;
        
        switch (state)
        {
            case State.Patrol: PatrolLogic(); break;
            case State.Chase: ChaseLogic(); break;
            case State.Attack: AttackLogic(); break;
        }
    }

    //** PATROL **
    void PatrolLogic()
    {
        if (CanSeePlayer()) { state = State.Chase; return; }

        if (patrolPoints == null || patrolPoints.Length == 0) return;

        if (!agent.pathPending && agent.remainingDistance < 0.3f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= patrolWaitTime)
            {
                index = (index + 1) % patrolPoints.Length;

                agent.SetDestination(patrolPoints[index].position);
                waitTimer = 0f;
            }
        }
    }

    // ** CHASE **
    void ChaseLogic()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > chaseRadius)
        {
            state = State.Patrol;
            if (patrolPoints.Length > 0)
                agent.SetDestination(patrolPoints[index].position); return;
        }

        if (dist <= attackRadius)
        {
            state = State.Attack;
            agent.isStopped = true;
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    // ** ATTACK **
    void AttackLogic()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > attackRadius)
        {
            state = State.Chase;
            agent.isStopped = false;
            return;
        }

        Vector3 lookDir = (player.position - transform.position).normalized;
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);

        fireTimer += Time.deltaTime;
        if (fireTimer >= 1f / fireRate)
        {
            fireTimer = 0f;
            FireHitscan();
        }
    }

    // ** AMPUU PELAAJAA **
    void FireHitscan()
    {
        if (shotClip)
            AudioSource.PlayClipAtPoint(shotClip, transform.position);
        
        Ray ray = new Ray(transform.position + Vector3.up * 1.5f, (player.position + Vector3.up) - (transform.position + Vector3.up * 1.5f));

        if (Physics.Raycast(ray, out RaycastHit hit, attackRadius + 1f, sightLayers))
        {
            IDamageable_JA dmg = hit.collider.GetComponent<IDamageable_JA>() ?? hit.collider.GetComponentInParent<IDamageable_JA>();
            if (dmg != null)
                dmg.TakeDamage(bulletDamage);
        }

        Debug.DrawRay(ray.origin, ray.direction * (attackRadius + 1f), Color.yellow, 2f);
    }

    // ** NÄKEE PELAAJAN **
    bool CanSeePlayer()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > sightRadius) return false;

        Vector3 dir = (player.position + Vector3.up) - (transform.position + Vector3.up * 1.5f);

        return !Physics.Raycast(transform.position + Vector3.up * 1.5f, dir.normalized, dist, sightLayers);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
