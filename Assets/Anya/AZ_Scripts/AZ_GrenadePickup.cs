using UnityEngine;

public class AZ_GrenadePickup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int grenadeAmount = 1;

    void OnTriggerEnter(Collider other)
    {
        AZ_GrenadeThrow player = other.GetComponent<AZ_GrenadeThrow>();
        if (player != null)
        {
            player.AddGrenades(grenadeAmount);
            Destroy(gameObject); // убираем ящик после подбора
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
