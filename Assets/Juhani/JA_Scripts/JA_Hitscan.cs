using UnityEngine;

    // tässä se interface nyt tehdään
    public interface IDamageable_JA
    {
        void TakeDamage(float damageAmount);
    }

public class JA_Hitscan : MonoBehaviour
{
    public float range = 100.0f;
    public float damage = 25.0f;
    public Camera fpsCamera;
    public LayerMask shootingLayer;

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
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, shootingLayer))
        {

            // Debuggausta, piirrellään drawray, kun osutaan kohteeseen
            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red, 2.0f);

            // checkaa jos objekti on sellainen mihin voi osua
            IDamageable_JA damageable = hit.transform.GetComponent<IDamageable_JA>();

            if (damageable != null)

            {
                damageable.TakeDamage(damage);
            }
        }
    }
}
