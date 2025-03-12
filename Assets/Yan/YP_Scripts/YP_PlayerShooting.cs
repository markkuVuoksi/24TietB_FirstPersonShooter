using UnityEngine;
public interface IDamageable_YP

{

    void TakeDamage(float damageAmount);

}
public class YP_PlayerShooting : MonoBehaviour
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

            //Debugging, drawing a drawray when hitting a target

            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red, 2.0f);

            //check if the object is one that can be hit

            IDamageable_YP damageable = hit.transform.GetComponent<IDamageable_YP>();

            if (damageable != null)

            {

                damageable.TakeDamage(damage);

            }

        }

    }
}

