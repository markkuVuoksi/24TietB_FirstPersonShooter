using UnityEngine;
using System.Collections;
using TMPro;

// here we make the interface now

public interface IDamageableMM

{

    void TakeDamage(float damageAmount);

}

public class MM_Shooting : MonoBehaviour
{
    public float range = 500.0f;

    public float damage = 25.0f;
    public float load = 30.0f;

    public TMP_Text bullet;

    public Camera fpsCamera;

    public LayerMask shootingLayer;

    //variable for waiting to shoot
    public bool canShoot = false;
    public float waitToShoot = 2f;

    public ParticleSystem gunFlash;
    public MM_AudioManager audioManager;
    public MM_PlayerMovement playerMovement;
    public MM_Enemy _Enemy;


    void Start()
    {
        canShoot = true;
        audioManager = Object.FindAnyObjectByType<MM_AudioManager>();
        playerMovement = Object.FindAnyObjectByType<MM_PlayerMovement>();
        _Enemy = Object.FindAnyObjectByType<MM_Enemy>();
    }

    void Update()

    {

        if (Input.GetButtonDown("Fire1") && canShoot && load > 0 && !playerMovement.GameOver && !_Enemy.YouWin)

        {

            Shoot();
            load--;
            ShootAnimation();
            audioManager.PlayGunSound();

            Debug.Log("Shoot");

        }
        UpdateBullet();

    }
    
    void UpdateBullet()
    {
        bullet.text = "Bullet : " + load.ToString();
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
