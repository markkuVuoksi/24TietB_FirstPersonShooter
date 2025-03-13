using UnityEngine;

public class JJP_Granadethrow : MonoBehaviour
{
    public GameObject grenadePrefab;

    public Camera playerCamera;

    public float throwForce = 10f;
    private void Awake()

    {

        if (!playerCamera)

        {

            Debug.LogError("Assign a Camera for the script in the inspector");

        }

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void ThrowGrenadeJimi()

    {

        GameObject grenade = Instantiate(grenadePrefab, playerCamera.transform.position + 			playerCamera.transform.forward, Quaternion.identity);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        if (rb != null)

        {

            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);

        }

    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButtonDown("Fire2") && grenadePrefab != null)

        {

            ThrowGrenadeJimi();

        } 
    }
}
