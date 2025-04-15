using UnityEngine;
public interface IDamageable_Jimi

{

    void TakeDamageJimi(float damageAmount);

}

public class JJP_Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;           // Reference to the bullet prefab
    public float shootForce = 10.0f;            // The force applied when shooting the bullet
    public Camera fpsCameraJimi;             // Camera to shoot from
    public LayerMask shootingLayerJimi;      // The layer mask for shooting (e.g., obstacles, enemies, etc.)

    void ShootJimi()
    {
        Vector3 spawnPosition = fpsCameraJimi.transform.position + fpsCameraJimi.transform.forward;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // Set bullet's rotation to match the camera's forward direction
        bullet.transform.rotation = Quaternion.LookRotation(fpsCameraJimi.transform.forward);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(fpsCameraJimi.transform.forward * shootForce, ForceMode.VelocityChange);
        }
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Fire1 is usually left mouse button or a custom input
        {
            ShootJimi();
        }
    }
}