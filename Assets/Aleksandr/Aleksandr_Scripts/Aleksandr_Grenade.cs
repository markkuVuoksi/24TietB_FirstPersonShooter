using System.Collections;
using UnityEngine;

public class Aleksandr_Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float blastRadius = 5f;
    public float explosionForce = 700f;
    public float damageAmount = 50f;
    public LayerMask damageableLayer;  // Убедитесь, что враги на нужном слое

    public AudioClip explosionSound;
    private bool hasExploded = false;

    void Start()
    {
        StartCoroutine(ExplodeAfterDelay());
    }

    IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        Explode();
    }

    void Explode()
    {
        if (hasExploded) return;

        // Получаем все коллайдеры в радиусе взрыва
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius, damageableLayer);
        foreach (Collider nearbyObject in colliders)
        {
            // Проверяем, есть ли у объекта Rigidbody, если есть — применяем силу взрыва
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius);
            }

            // Пытаемся получить интерфейс IDamageable (который должен быть у врагов)
            IDamageableAM damageable = nearbyObject.GetComponent<IDamageableAM>();  // Здесь используем интерфейс из скрипта врага
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);  // Наносим урон
            }
        }

        // Воспроизводим звук взрыва, если он есть
        if (explosionSound != null)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        // Отмечаем, что граната взорвалась, чтобы не повторилось
        hasExploded = true;

        // Удаляем объект гранаты
        Destroy(gameObject);
    }
}
