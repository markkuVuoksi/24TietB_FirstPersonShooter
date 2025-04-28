using UnityEngine;

public class SoundToggle : MonoBehaviour
{
    public void TurnOnSound()
    {
        AudioListener.pause = false;
        PlayerPrefs.SetInt("Muted", 0);
        Debug.Log("Âm thanh: BẬT");
    }

    public void TurnOffSound()
    {
        AudioListener.pause = true;
        PlayerPrefs.SetInt("Muted", 1);
        Debug.Log("Âm thanh: TẮT");
    }

    void Start()
    {
        bool isMuted = PlayerPrefs.GetInt("Muted", 0) == 1;
        AudioListener.pause = isMuted;
    }
}
