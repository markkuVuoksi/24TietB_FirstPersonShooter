using UnityEngine;

public class AZ_SmokeGrenade : AZ_BaseGrenade
{
    public GameObject smokeEffectPrefab;
    private bool hasExploded = false;
    public AudioClip explosionSound;

    private AudioSource audioSource;

    protected override  void Explode()
    {
        if (hasExploded) return;

        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }

        if (smokeEffectPrefab != null)
        {
            GameObject smoke = Instantiate(smokeEffectPrefab, transform.position, Quaternion.identity);
            Destroy(smoke, 10f); // ��� ������������ ����� 10 ������
        }

        hasExploded = true;
        Destroy(gameObject, 0.5f);
    }
}
