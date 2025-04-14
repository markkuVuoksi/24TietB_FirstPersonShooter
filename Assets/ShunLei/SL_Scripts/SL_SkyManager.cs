using UnityEngine;

public class SL_SkyManager : MonoBehaviour
{
    public float skySpeed;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skySpeed);
    }
}
