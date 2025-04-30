using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;
using TMPro;

public class MM_ThrowGrenade : MonoBehaviour
{
    
    public GameObject grenadePrefab;

    public Camera playerCamera;

    public TMP_Text count;

    public float throwForce = 10f;
    public float throwCooldown = 3f;
    public float grenadeCount = 3;
    public bool canThrow = false;

    public MM_PlayerMovement _PlayerMovement;
    public MM_Enemy _Enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canThrow = true;
        _Enemy = Object.FindAnyObjectByType<MM_Enemy>();
        _PlayerMovement = Object.FindAnyObjectByType<MM_PlayerMovement>();
    }
    private void Awake()

    {

        if (!playerCamera)

        {

            Debug.LogError("Assign a Camera for the script in the inspector");

        }

    }



    void Update()

    {

        if (Input.GetButtonDown("Fire2") && grenadePrefab != null && canThrow && grenadeCount > 0 && !_PlayerMovement.GameOver && !_Enemy.YouWin)

        {

            ThrowGrenade();
            grenadeCount--;

        }
        UpdateGrenadeCount();
    }
    void UpdateGrenadeCount()
    {
        count.text = "Grenade : " + grenadeCount.ToString();
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
