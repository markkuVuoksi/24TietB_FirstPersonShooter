using UnityEngine;

public class Aleksandr_Hitbox : MonoBehaviour
{
    public float damageMultiplier = 1.0f; 

    private IDamageableAM damageableParent;

    void Start()
    {
        damageableParent = GetComponentInParent<IDamageableAM>();
    }

    public void ApplyDamage(float baseDamage)
    {
        if (damageableParent != null)
        {
            float finalDamage = baseDamage * damageMultiplier;
            damageableParent.TakeDamage(finalDamage);
        }
    }
}
