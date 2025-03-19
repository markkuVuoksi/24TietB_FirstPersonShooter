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
        // Instantiate the bullet at the camera's position, slightly in front of the camera
        Vector3 spawnPosition = fpsCameraJimi.transform.position + fpsCameraJimi.transform.forward;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // Make the bullet face the same direction as the camera
        bullet.transform.rotation = Quaternion.LookRotation(fpsCameraJimi.transform.up);

        // Get the Rigidbody component of the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Add force to the bullet to make it move forward in the direction it's facing
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