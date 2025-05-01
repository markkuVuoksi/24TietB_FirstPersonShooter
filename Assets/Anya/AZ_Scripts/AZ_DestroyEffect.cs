using UnityEngine;

public class AZ_DestroyEffect : MonoBehaviour

{
    public GameObject explosionEffectPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
