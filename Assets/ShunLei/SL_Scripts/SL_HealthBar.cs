using UnityEngine;
using UnityEngine.UI;

public class SL_HealthBar : MonoBehaviour
{
    // public SL_Enemy healthSystem;
    // //public Image healthFill;
    // public Vector3 offset = new Vector3(0, 2, 0); // Offset above object


    // void Update()
    // {
    //     if (healthSystem != null)
    //     {
    //         // // Update health bar fill amount
    //         // healthFill.fillAmount = healthSystem.health/100;;

    //         // Debug.Log("fill amount" + healthSystem.health);

    //         // Follow the object
    //         transform.position = healthSystem.transform.position + offset;
    //     }
    // }

    public Slider slider;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue/maxValue;
    }
}
