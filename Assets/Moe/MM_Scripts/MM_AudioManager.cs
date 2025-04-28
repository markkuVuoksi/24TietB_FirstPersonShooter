using UnityEngine;
using System.Collections;

public class MM_AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootingSound;
    public AudioClip bombingSound;
    public AudioClip loadingSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(audioSource !=null)

        { 
            audioSource = GetComponent<AudioSource>();
        }
        
    }
    public void PlayGunSound()
    {
        if (audioSource && shootingSound && loadingSound)
        {
            StartCoroutine(PlayLoadingAfterGun());
        }
    }

    private IEnumerator PlayLoadingAfterGun()
    {
        audioSource.PlayOneShot(shootingSound);

        yield return new WaitForSeconds(1);

        audioSource.PlayOneShot(loadingSound);
    }
    
    public void PlayBombSound()
    {
        if(audioSource && bombingSound)
        {
            audioSource.PlayOneShot(bombingSound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
