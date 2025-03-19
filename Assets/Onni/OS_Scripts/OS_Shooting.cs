using UnityEngine;
public interface OS_IDamageable
{
    void TakeDamage(float damageAmount);
}
public class OS_Shooting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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
            // Debuggausta, piirrellään drawray, kun osutaan kohteeseen
            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red, 2.0f);
            // checkaa jos objekti on sellainen mihin voi osua
            OS_IDamageable damageable = hit.transform.GetComponent<OS_IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
             
        }

    }
}
