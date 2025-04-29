using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class MM_VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider volSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!PlayerPrefs.HasKey("VolumeSlider"))
        {
            PlayerPrefs.SetFloat("VolumeSlider", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    public void ChangeVolume()
    {
        AudioListener.volume = volSlider.value;
        Save();
    }
    private void Load()
    {
        volSlider.value = PlayerPrefs.GetFloat("VolumeSlider");
    }
    // Update is called once per frame
    private void Save()
    {
        PlayerPrefs.SetFloat("VolumeSlider", volSlider.value);
    }
}
