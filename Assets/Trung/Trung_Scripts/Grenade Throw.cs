using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
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
        Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.forward * 1.5f;
        GameObject branched = Instantiate(grenadePrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = branched.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);
        }
    }
}
