using UnityEngine;
using TMPro;
public interface IDamageableAZ

{

    void TakeDamage(float damageAmount);

}


public class AZ_PlayerShooting : MonoBehaviour
{
    public float range = 100.0f;              

    public float damage = 25.0f;              

    public Camera fpsCamera;                  

    public LayerMask shootingLayer;
    public ParticleSystem muzzleFlash;
    public AudioClip gunSound;         
    public AudioSource audioSource;

    public int maxAmmo = 10;                  // Максимальное количество патронов
    public int currentAmmo;                   // Текущее количество патронов
    public KeyCode reloadKey = KeyCode.R;     // Кнопка перезарядки

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI targetHealthText;
    

    public GameObject hitEffectPrefab;

    void Start()
    {
        currentAmmo = maxAmmo; // Заполнить при старте
        UpdateUI();
    }

    void Update()

    {

        if (Input.GetButtonDown("Fire1"))

        {

            if (currentAmmo > 0)
            {
                Shoot();
            }
            else
            {
                Debug.Log("Out of ammo! Press 'R' to reload.");
            }

        }

        if (Input.GetKeyDown(reloadKey))
        {
            Reload();
        }

    }

    void Shoot()

    {
        currentAmmo--;
 
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();

        }

        if (gunSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gunSound);
        }

        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, shootingLayer))

        {

            // Debugging, drawing a drawray when hitting an object

            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red, 2.0f);

            // check if the object is hittable

            IDamageableAZ damageable = hit.transform.GetComponent<IDamageableAZ>();

            if (damageable != null)

            {

                damageable.TakeDamage(damage);

                float remainingHealth = GetHealthFromTarget(hit.transform.gameObject);
                if (targetHealthText != null)
                {
                    targetHealthText.text = "Target Health: " + remainingHealth.ToString("F0");
                }

                if (hitEffectPrefab != null)
                {
                    Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                    
                }

            }

        }
        UpdateUI();

    }
    void Reload()
    {
        currentAmmo = maxAmmo;
        Debug.Log("Reloaded!");
        UpdateUI();
    }

    void UpdateUI()
    {
        if (ammoText != null)
        {
            ammoText.text = "Ammo: " + currentAmmo + "/" + maxAmmo;
        }
    }
    float GetHealthFromTarget(GameObject target)
    {
        // Предполагаем, что у цели есть компонент с публичным полем currentHealth
        var healthComponent = target.GetComponent<AZ_Enemy>();
        if (healthComponent != null)
        {
            return healthComponent.health;
        }

        return 0;
    }
}


