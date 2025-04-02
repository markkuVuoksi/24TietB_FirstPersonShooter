using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Rendering.VirtualTexturing;

public interface IDamageableAM

{

    void TakeDamage(float damageAmount);

}

public class Aleksandr_Shooting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

          

            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red, 2.0f);

           

                        IDamageableAM damageable = hit.transform.GetComponent<IDamageableAM>();

            if (damageable != null)

            {

                damageable.TakeDamage(damage);

            }

        }

    }
}
