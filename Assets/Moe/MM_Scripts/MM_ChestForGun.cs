using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MM_ChestForGun : MonoBehaviour
{
    private Rigidbody rb;
    public MM_Shooting shooting;
    public MM_ThrowGrenade throwGrenade;
    public MM_AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        shooting = Object.FindAnyObjectByType<MM_Shooting>();
        throwGrenade = Object.FindAnyObjectByType<MM_ThrowGrenade>();
        audioManager = Object.FindAnyObjectByType<MM_AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            shooting.load += 10;
            throwGrenade.grenadeCount += 2;
            audioManager.audioSource.PlayOneShot(audioManager.loadingSound);
            Debug.Log("Your bullets is" + shooting.load);
        }
    }
}
