using UnityEngine;

    // tässä se interface nyt tehdään
    public interface IDamageable_JA
    {
        void TakeDamage(float damageAmount);
    }

public class JA_Hitscan : MonoBehaviour
{
    [Header("Ase-asetukset")]
    public float range = 100.0f;
    public float damage = 25.0f;
    public Camera fpsCamera;
    public LayerMask shootingLayer = 0;

    [Header("Ääni")]
    public AudioClip gunshot;
    public float gunShotVolume = 1f;

    void Update()
    {
        //Debug.Log("shoot_update");
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("shoot");
            Shoot();
        }
    }

    void Shoot()
    {
        if (fpsCamera == null)
        {
            Debug.LogError("fpsCamera on null");
            return;
        }

        if (gunshot != null)
            AudioSource.PlayClipAtPoint(gunshot, transform.position, gunShotVolume);

        Camera cam = fpsCamera != null ? fpsCamera : Camera.main;
        if (cam == null)
        {
            Debug.Log("Ei kameraa!");
            return;
        }

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            // Debuggausta, piirrellään drawray, kun osutaan kohteeseen
            Debug.Log("Hit: " + hit.transform.name);
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 1.0f);
            

            // checkaa jos objekti on sellainen mihin voi osua
            IDamageable_JA damageable = hit.collider.GetComponent<IDamageable_JA>() ?? hit.collider.GetComponentInParent<IDamageable_JA>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
