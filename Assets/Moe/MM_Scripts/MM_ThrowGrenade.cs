using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class MM_ThrowGrenade : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canThrow = true;
    }
    public GameObject grenadePrefab;

    public Camera playerCamera;

    public float throwForce = 10f;
    public float throwCooldown = 3f;
    public bool canThrow = false;

    
    private void Awake()

    {

        if (!playerCamera)

        {

            Debug.LogError("Assign a Camera for the script in the inspector");

        }

    }



    void Update()

    {

        if (Input.GetButtonDown("Fire2") && grenadePrefab != null && canThrow)

        {

            ThrowGrenade();

        }

    }



    void ThrowGrenade()

    {
        canThrow = false;
        GameObject Grenade = Instantiate(grenadePrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);

        Rigidbody rb = Grenade.GetComponent<Rigidbody>();

        if (rb != null)

        {

            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);

        }
        Debug.Log("The Bomb is on cooldown");
        StartCoroutine(ResetThrowCoolDown());

    }

    IEnumerator ResetThrowCoolDown()
    {
        yield return new WaitForSeconds(throwCooldown);
        canThrow = true;

    }

}
