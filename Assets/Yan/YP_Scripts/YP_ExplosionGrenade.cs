using UnityEngine;
using System.Collections;

public class YP_ExplosionGrenade : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float delay = 3f;

    public float blastRadius = 5f;

    public float explosionForce = 700f;

    public float damageAmount = 50f;

    public LayerMask damageableLayer;



    private bool hasExploded = false;



    void Start()

    {

        StartCoroutine(ExplodeAfterDelay());

    }



    IEnumerator ExplodeAfterDelay()

    {

        yield return new WaitForSeconds(delay);

        Explode();

    }



    void Explode()

    {

        if (hasExploded) return;



        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius, damageableLayer);

        foreach (Collider nearbyObject in colliders)

        {

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();

            if (rb != null)

            {

                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);

            }



            IDamageable_YP damageable = nearbyObject.GetComponent<IDamageable_YP>();

            if (damageable != null)

            {

                damageable.TakeDamage(damageAmount);

            }

        }



        hasExploded = true;

        Destroy(gameObject);

    }
}
