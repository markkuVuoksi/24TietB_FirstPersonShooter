using UnityEngine;

public class JJP_Shooting : MonoBehaviour
{
    public float rangeJimi = 100.0f;
    public float damageJimi = 25.0f;
    public Camera fpsCameraJimi;
    public LayerMask shootingLayerJimi;
    public GameObject grenadePrefab;

    public Camera playerCamera;

    public float throwForce = 10f;

    private void Awake()

    {

        if (!playerCamera)

        {

            Debug.LogError("Assign a Camera for the script in the inspector");

        }

    }
    

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
        if (Input.GetButtonDown("Fire2") && grenadePrefab != null)

        {

            ThrowGrenadejIMI();

        }
        if (Input.GetButtonDown("Fire1"))
        {
            ShootJimi();
        }
    }
    void ThrowGrenadejIMI()

    {

        GameObject grenade = Instantiate(grenadePrefab, playerCamera.transform.position + 			playerCamera.transform.forward, Quaternion.identity);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        if (rb != null)

        {

            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);

        }

    }
}
