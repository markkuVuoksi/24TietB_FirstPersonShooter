using UnityEngine;
using System.Collections;

public class Aleksandr_GrenadeThrow : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Camera playerCamera;
    public float throwForce = 10f;
    public float throwCooldown = 2f; 
    private bool canThrow = true; 

    private void Awake()
    {
        if (!playerCamera)
        {
            Debug.LogError("Назначьте камеру для скрипта в инспекторе");
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
        
        GameObject grenade = Instantiate(grenadePrefab, playerCamera.transform.position + playerCamera.transform.forward, Quaternion.identity);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);
        }

        
        StartCoroutine(ThrowCooldown());
    }

    IEnumerator ThrowCooldown()
    {
        canThrow = false; 
        yield return new WaitForSeconds(throwCooldown); 
        canThrow = true; 
    }
}
