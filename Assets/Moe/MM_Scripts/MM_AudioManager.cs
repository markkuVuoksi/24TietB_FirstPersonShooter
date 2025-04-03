using UnityEngine;

public class MM_AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
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
        if (audioSource && audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void GrenadeExplowSound()
    {
        if (audioSource && audioClip)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
