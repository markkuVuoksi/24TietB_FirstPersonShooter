using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class AZ_GrenadeThrow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
   

    public Camera playerCamera;

    public float throwForce = 10f;

    public float throwCooldown = 1f;

    private float lastThrowTime = -Mathf.Infinity;
    
    public TextMeshProUGUI grenadeText;
    public int maxGrenades = 3;
    public int currentGrenades = 3;
    public enum GrenadeType { Explosive, Smoke }

    public GameObject explosiveGrenadePrefab;
    public GameObject smokeGrenadePrefab;

    private GrenadeType selectedGrenade = GrenadeType.Explosive;

    public Image grenadeIcon;
    public Sprite explosiveIcon;
    public Sprite smokeIcon;

    public TextMeshProUGUI grenadeTypeText;




    private void Awake()

    {

        if (!playerCamera)

        {

            Debug.LogError("Assign a Camera for the script in the inspector");

        }

    }

    public void SwitchGrenadeType()
    {
        selectedGrenade = selectedGrenade == GrenadeType.Explosive ? GrenadeType.Smoke : GrenadeType.Explosive;
    }


    void Update()

    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectedGrenade = selectedGrenade == GrenadeType.Explosive ? GrenadeType.Smoke : GrenadeType.Explosive;
        }


        if (Input.GetButtonDown("Fire2") && Time.time >= lastThrowTime + throwCooldown && currentGrenades > 0)

        {
            ThrowGrenade();
            lastThrowTime = Time.time;
        }
        UpdateUI();
    }



    void ThrowGrenade()

    {
        GameObject prefabToThrow = selectedGrenade == GrenadeType.Explosive ? explosiveGrenadePrefab : smokeGrenadePrefab;
        GameObject Grenade = Instantiate(prefabToThrow, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);
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
        //if (grenadeText != null)
        //{
        //    grenadeText.text = $"Grenades: {currentGrenades}/{maxGrenades}";
        //}
       
        if (grenadeText != null)
        {
            grenadeText.text = $"Grenades: {currentGrenades}/{maxGrenades}";
        }

        if (grenadeTypeText != null)
        {
            grenadeTypeText.text = selectedGrenade.ToString();
        }

        if (grenadeIcon != null)
        {
            grenadeIcon.sprite = selectedGrenade == GrenadeType.Explosive ? explosiveIcon : smokeIcon;
        }
    }
}
