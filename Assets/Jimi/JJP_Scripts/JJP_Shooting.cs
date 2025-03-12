using UnityEngine;

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
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();

            if (damageable != null)
            {
                // Call the TakeDamage method (defined in IDamageable) instead of TakeDamageJimi
                damageable.TakeDamage(damageJimi);
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
