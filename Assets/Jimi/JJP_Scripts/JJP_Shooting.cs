using UnityEngine;
public interface IDamageable_Jimi

{

    void TakeDamageJimi(float damageAmount);

}

public class JJP_Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;           // Reference to the bullet prefab
    public float shootForce = 20f;            // The force applied when shooting the bullet
    public Camera fpsCameraJimi;             // Camera to shoot from
    public LayerMask shootingLayerJimi;      // The layer mask for shooting (e.g., obstacles, enemies, etc.)

    void ShootJimi()
    {
        // Instantiate the bullet at the camera's position and direction
        GameObject bullet = Instantiate(bulletPrefab, fpsCameraJimi.transform.position + fpsCameraJimi.transform.forward, Quaternion.identity);

        // Get the Rigidbody component of the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Add force to the bullet to make it move forward
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