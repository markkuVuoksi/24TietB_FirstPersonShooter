using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_BackGroundSound : MonoBehaviour
{
    [Header("Background Music Clip Index")]
    public int musicClipIndex; // Background music index for this scene

    private void Start()
    {
        // Play the specific background music for this scene
        M_SoundManagerWholeScene.Instance.PlayBackgroundMusic(musicClipIndex);
    }
}
