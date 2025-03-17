using UnityEngine;

public class SL_GrenadeThrow : MonoBehaviour
{
    public GameObject grenadePrefab;

    public Camera playerCamera;

    public float throwForce = 10f;

    
    public AudioSource audioSoruce;



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
             

            //Play throwing sound
            audioSoruce.Play();

            ThrowGrenade();

        }

    }



    void ThrowGrenade()

    {

        GameObject grenade = Instantiate(grenadePrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);			

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        if (rb != null)

        {

            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);

        }

    }
}
