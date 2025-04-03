using System.Collections;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Camera playerCamera;
    public float throwForce = 10f;
    public AudioClip destroySound; // Âm thanh khi lựu đạn bị phá hủy
    public GameObject explosionEffect; // Prefab của hiệu ứng nổ

    private void Awake()
    {
        if (!playerCamera)
        {
            Debug.LogError("Assign a Camera for the script in the inspector");
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && grenadePrefab != null)
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        // Tạo lựu đạn
        Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.forward * 1.5f;
        GameObject grenade = Instantiate(grenadePrefab, spawnPosition, Quaternion.identity);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(playerCamera.transform.forward * throwForce, ForceMode.VelocityChange);
        }

        // Sau khi lựu đạn bị hủy, tạo hiệu ứng nổ và âm thanh
        StartCoroutine(PlayExplosion(grenade));
    }

    private IEnumerator PlayExplosion(GameObject grenade)
    {
        // Đợi một chút cho đến khi lựu đạn nổ
        yield return new WaitForSeconds(3f); // Lựu đạn sẽ nổ sau 3 giây

        // Kiểm tra xem lựu đạn có còn tồn tại hay không
        if (grenade != null)
        {
            // Debug kiểm tra vị trí nổ
            Debug.Log("Grenade exploded at position: " + grenade.transform.position);

            // Hiệu ứng hình ảnh nổ
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, grenade.transform.position, Quaternion.identity);
                Debug.Log("Explosion effect instantiated.");
            }
            else
            {
                Debug.LogWarning("Explosion effect is null.");
            }

            // Phát âm thanh
            if (destroySound != null)
            {
                AudioSource.PlayClipAtPoint(destroySound, grenade.transform.position);
                Debug.Log("Explosion sound played.");
            }
            else
            {
                Debug.LogWarning("Destroy sound is null.");
            }

            // Hủy lựu đạn sau khi nổ
            Destroy(grenade);
        }
        else
        {
            Debug.LogWarning("Grenade object is null when trying to play explosion.");
        }
    }
}
