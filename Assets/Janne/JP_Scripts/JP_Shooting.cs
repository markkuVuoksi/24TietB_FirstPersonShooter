using UnityEngine;
public interface IDamageable
    {

        void TakeDamage(float damageAmount);

    }

public class JP_Shooting : MonoBehaviour
{
    public float range = 100.0f;

    public float damage = 25.0f;

    public Camera fpsCamera;

    public LayerMask shootingLayer;

    

    void Update()

    {

        if (Input.GetButtonDown("Fire1"))

        {

            Shoot();

        }

    }



    void Shoot()

    {

        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, shootingLayer))

        {

            // Debuggausta, piirrellään drawray, kun osutaan kohteeseen

            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red, 2.0f);

            // checkaa jos objekti on sellainen mihin voi osua

            IDamageable damageable = hit.transform.GetComponent<IDamageable>();

            if (damageable != null)

            {

                damageable.TakeDamage(damage);

            }

        }

    }
    // tässä se interface nyt tehdään


}
