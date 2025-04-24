using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class M_SoundManagerWholeScene : MonoBehaviour
{
    public static M_SoundManagerWholeScene Instance;

    [Header("Audio Sources")]
    public AudioSource backgroundMusicSource; // For background music
    public AudioSource sfxSource;            // For sound effects

    [Header("Audio Clips")]
    public AudioClip[] backgroundMusicClips; // Store background music tracks
    public AudioClip[] sfxClips;             // Store various SFX clips (e.g., button clicks, collision sounds)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("SoundManager created and persisting across scenes.");
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("Duplicate SoundManager destroyed.");
        }
    }

    // Play background music
    // Play background music if not already playing
    public void PlayBackgroundMusic(int clipIndex)
    {
        if (backgroundMusicSource.isPlaying && backgroundMusicSource.clip == backgroundMusicClips[clipIndex])
        {
            return; // Prevent restarting if the same clip is already playing
        }

        if (clipIndex >= 0 && clipIndex < backgroundMusicClips.Length)
        {
            backgroundMusicSource.clip = backgroundMusicClips[clipIndex];
            backgroundMusicSource.Play();
        }
    }

    // Stop background music
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }

    // Play a single sound effect
    public void PlaySFX(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[clipIndex]);
        }
    }

    // Play a sound effect with a custom clip
    public void PlaySFXCustom(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
