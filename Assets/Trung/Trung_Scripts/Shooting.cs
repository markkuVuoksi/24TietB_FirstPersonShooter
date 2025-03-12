using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Rendering.VirtualTexturing;

public class Shooting : MonoBehaviour
{
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
            
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();

            if (damageable != null)
            {

                damageable.TakeDamage(damage);
            }
        }
    }
    public interface IDamageable

    {
        void TakeDamage(float damageAmount);
    }
}
