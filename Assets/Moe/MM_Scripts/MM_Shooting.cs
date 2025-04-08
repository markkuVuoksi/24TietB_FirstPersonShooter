using UnityEngine;
using System.Collections;

// here we make the interface now

public interface IDamageableMM

{

    void TakeDamage(float damageAmount);

}

public class MM_Shooting : MonoBehaviour
{
    public float range = 500.0f;

    public float damage = 25.0f;

    public Camera fpsCamera;

    public LayerMask shootingLayer;

    //variable for waiting to shoot
    public bool canShoot = false;
    public float waitToShoot = 1.5f;

    public ParticleSystem gunFlash;
    public MM_AudioManager audioManager;


    void Start()
    {
        canShoot = true;
        audioManager = Object.FindAnyObjectByType<MM_AudioManager>();
    }

    void Update()

    {

        if (Input.GetButtonDown("Fire1") && canShoot)

        {

            Shoot();
            ShootAnimation();
            audioManager.PlayGunSound();

            Debug.Log("Shoot");

        }

    }

    void Shoot()

    {
        canShoot = false;

        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, shootingLayer))

        {

            // Debugging, drawing a drawray when hitting an object

            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red, 2.0f);

            // check if the object is hittable

            IDamageableMM damageable = hit.transform.GetComponent<IDamageableMM>();

            if (damageable != null)

            {

                damageable.TakeDamage(damage);

            }

        }
        StartCoroutine(WaitToShoot());

        

    }

    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(waitToShoot);
        canShoot = true;
    }
    void ShootAnimation()
    {
        if (gunFlash != null)
        {
            Debug.Log("Animation Triggered");
            gunFlash.Play();
        }
    }

  
}
