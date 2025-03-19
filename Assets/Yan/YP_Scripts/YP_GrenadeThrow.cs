using UnityEngine;

public class YP_GrenadeThrow : MonoBehaviour
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



    void Update()

    {

        if (Input.GetButtonDown("Fire2") && grenadePrefab != null)

        {

            ThrowGrenade();

        }

    }



    void ThrowGrenade()

    {

        GameObject branched = Instantiate(grenadePrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);

        Rigidbody rb = branched.GetComponent<Rigidbody>();

        if (rb != null)

        {

            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);

        }

    }
}
