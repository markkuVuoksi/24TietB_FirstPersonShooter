using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float range = 100.0f;
    public float damage = 25.0f;
    public Camera fpsCamera;
    public LayerMask shootingLayer;
    public GameObject laserTrailPrefab; // Prefab chứa Trail Renderer

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Vector3 shootOrigin = fpsCamera.transform.position;
        Vector3 shootDirection = fpsCamera.transform.forward;

        if (Physics.Raycast(shootOrigin, shootDirection, out hit, range, shootingLayer))
        {
            // Gây sát thương cho kẻ địch
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }

            // Tạo vệt laser từ súng đến vị trí trúng
            SpawnLaserTrail(shootOrigin, hit.point);
        }
        else
        {
            // Nếu không trúng, bắn laser đến khoảng cách tối đa
            SpawnLaserTrail(shootOrigin, shootOrigin + shootDirection * range);
        }
    }

    void SpawnLaserTrail(Vector3 start, Vector3 end)
    {
        GameObject laserTrail = Instantiate(laserTrailPrefab, start, Quaternion.identity);
        TrailRenderer trail = laserTrail.GetComponent<TrailRenderer>();

        if (trail != null)
        {
            ConfigureTrail(trail);
            StartCoroutine(MoveTrail(trail, start, end));
        }
    }

    void ConfigureTrail(TrailRenderer trail)
    {
        // Thiết lập thông số Trail Renderer
        trail.startWidth = 0.2f;
        trail.endWidth = 0f;
        trail.time = Random.Range(0.2f, 0.5f); // Ngẫu nhiên trong khoảng 0.2 - 0.5 giây

        // Đặt màu sáng (đỏ - xanh) bằng Color Gradient
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.red, 0.0f),
                new GradientColorKey(Color.cyan, 1.0f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(0.0f, 1.0f)
            }
        );

        trail.colorGradient = gradient;
    }

    IEnumerator MoveTrail(TrailRenderer trail, Vector3 start, Vector3 end)
    {
        float duration = 0.05f; // Thời gian di chuyển
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            trail.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        trail.transform.position = end;
        Destroy(trail.gameObject, trail.time); // Xóa sau khi trail hoàn tất
    }
}
