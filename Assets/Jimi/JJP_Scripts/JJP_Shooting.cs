using UnityEngine;
public interface IDamageable_Jimi

{

    void TakeDamageJimi(float damageAmount);

}

public class JJP_Shooting : MonoBehaviour
{
    public float rangeJimi = 100.0f;
    public float damageJimi = 25.0f;
    public Camera fpsCameraJimi;
    public LayerMask shootingLayerJimi;

    

    void ShootJimi()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCameraJimi.transform.position, fpsCameraJimi.transform.forward, out hit, rangeJimi, shootingLayerJimi))
        {
            // Debugging: visualize ray
            Debug.DrawRay(fpsCameraJimi.transform.position, fpsCameraJimi.transform.forward * rangeJimi, Color.red, 2.0f);

            // Check if object is damageable
            IDamageable_Jimi damageable = hit.transform.GetComponent<IDamageable_Jimi>();

            if (damageable != null)

            {

                damageable.TakeDamageJimi(damageJimi);

            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootJimi();
        }
    }
}
