using UnityEngine;

public class JP_ThrowGrenade : MonoBehaviour
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

        GameObject grenade = Instantiate(grenadePrefab, playerCamera.transform.position + 			playerCamera.transform.forward, Quaternion.identity);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        if (rb != null)

        {

            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);

        }

    }
}
