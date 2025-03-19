using UnityEngine;

public class SL_MusicAndSoundManagement : MonoBehaviour
{
    private static SL_MusicAndSoundManagement instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Prevent multiple music players
        }
    }
}
