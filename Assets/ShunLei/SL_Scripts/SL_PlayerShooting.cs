using UnityEngine;

// here we make the interface now

public interface IDamageableSL

{

    void TakeDamage(float damageAmount);

}

public class SL_PlayerShooting : MonoBehaviour
{
    public float range = 100.0f;

    public float damage = 25.0f;

    public Camera fpsCamera;

    public LayerMask shootingLayer;

    public ParticleSystem explosionParticle;

    public AudioSource audioSoruce;
    public SL_EnemyManager sL_EnemyManager;
    private SL_PauseManager pauseManager;


    void Start()
    {
        sL_EnemyManager = GameObject.Find("EnemyManager").GetComponent<SL_EnemyManager>();
        //pauseManager = GameObject.Find("pause button").GetComponent<SL_PauseManager>();
    }

    void Update()

    {
        if (Input.GetButtonDown("Fire1") && !sL_EnemyManager.isGameEnd)
        {
            
            //Show particle effect when an object is hit
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

            //Play shooting sound
            audioSoruce.Play();

            Shoot();

        }


    }

    void Shoot()

    {

        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, shootingLayer))

        {

            // Debugging, drawing a drawray when hitting an object

            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red, 2.0f);

            // check if the object is hittable

            IDamageableSL damageable = hit.transform.GetComponent<IDamageableSL>();

            if (damageable != null)

            {

                damageable.TakeDamage(damage);

            }

        }

    }
}


