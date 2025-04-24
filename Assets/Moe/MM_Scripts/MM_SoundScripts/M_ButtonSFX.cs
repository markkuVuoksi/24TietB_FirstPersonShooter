using UnityEngine;
using UnityEngine.UI;

public class M_ButtonSFX : MonoBehaviour
{
    [Header("SFX Clip Index for Button")]
    public int sfxClipIndex; // Index of the SFX in the SoundManager

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => PlayButtonSFX());
        }
    }

    private void PlayButtonSFX()
    {
        if (M_SoundManagerWholeScene.Instance != null)
        {
            M_SoundManagerWholeScene.Instance.PlaySFX(sfxClipIndex);
        }
    }
}