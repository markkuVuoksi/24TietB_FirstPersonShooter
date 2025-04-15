using UnityEngine;

public interface IDamageableAM
{
    void TakeDamage(float damageAmount);
}

public class Aleksandr_Shooting : MonoBehaviour
{
    public float range = 100.0f;
    public float damage = 25.0f;
    public Camera fpsCamera;
    public LayerMask shootingLayer;

    public float sphereCastRadius = 1.0f; // Радиус сферы для надёжного попадания

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(fpsCamera.transform.position, fpsCamera.transform.forward);
        RaycastHit hit;

        // Сферический луч для попадания по движущимся врагам
        if (Physics.SphereCast(ray, sphereCastRadius, out hit, range, shootingLayer))
        {
            Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, Color.red, 2.0f);
            Debug.Log("Выстрел попал в: " + hit.collider.gameObject.name);

            // Если попали в хитбокс — передаём через него
            Aleksandr_Hitbox hitbox = hit.collider.GetComponent<Aleksandr_Hitbox>();
            if (hitbox != null)
            {
                hitbox.ApplyDamage(damage);
                return;
            }

            // Если не хитбокс — проверяем обычный компонент у родителя
            IDamageableAM damageable = hit.collider.GetComponentInParent<IDamageableAM>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }
            else
            {
                Debug.Log("Цель не имеет интерфейса IDamageableAM");
            }
        }
    }
}
