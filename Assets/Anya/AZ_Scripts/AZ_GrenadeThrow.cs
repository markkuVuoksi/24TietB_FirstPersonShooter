using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class AZ_GrenadeThrow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    public GameObject grenadePrefab;

    public Camera playerCamera;

    public float throwForce = 10f;

    public float throwCooldown = 1f;

    private float lastThrowTime = -Mathf.Infinity;
    
    public TextMeshProUGUI grenadeText;
    public int maxGrenades = 3;
    public int currentGrenades = 3;
   


    private void Awake()

    {

        if (!playerCamera)

        {

            Debug.LogError("Assign a Camera for the script in the inspector");

        }

    }



    void Update()

    {

        if (Input.GetButtonDown("Fire2") && grenadePrefab != null && Time.time >= lastThrowTime + throwCooldown && currentGrenades > 0)

        {
            ThrowGrenade();
            lastThrowTime = Time.time;
        }
        UpdateUI();
    }



    void ThrowGrenade()

    {

        GameObject Grenade = Instantiate(grenadePrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);
        Grenade.transform.Rotate(-90.0f, 0f, 0f);
        Rigidbody rb = Grenade.GetComponent<Rigidbody>();

        if (rb != null)

        {
            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);
         
        }
        currentGrenades--;

    }
    public void AddGrenades(int amount)
    {
        currentGrenades = Mathf.Clamp(currentGrenades + amount, 0, maxGrenades);
    }

    void UpdateUI()
    {
        if (grenadeText != null)
        {
            grenadeText.text = $"Grenades: {currentGrenades}/{maxGrenades}";
        }
    }
}
