using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float range = 100.0f;
    public float damage = 25.0f;
    public Camera fpsCamera;
    public LayerMask shootingLayer;
    public GameObject laserTrailPrefab; // Prefab chứa Trail Renderer
    public AudioClip laserSoundClip;
    public AudioClip musicBackground;
    private AudioSource audiosource;

    private float nextTimeToShoot = 0f; // Thời điểm được bắn tiếp theo
    private float shootCooldown = 0.5f; // Khoảng thời gian giữa các phát bắn

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = musicBackground;
        audiosource.loop = true;
        audiosource.Play();
    }

    private void Update()
    {
        // Không xử lý nếu đang tạm dừng
        if (Time.timeScale == 0f) return;

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + shootCooldown;
            Shoot();
            audiosource.PlayOneShot(laserSoundClip);
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        Vector3 shootOrigin = fpsCamera.transform.position;
        Vector3 shootDirection = fpsCamera.transform.forward;

        if (Physics.Raycast(shootOrigin, shootDirection, out hit, range, shootingLayer))
        {
            IDamageable damageable = hit.transform.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }

            SpawnLaserTrail(shootOrigin, hit.point);
        }
        else
        {
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
        trail.startWidth = 0.2f;
        trail.endWidth = 0f;
        trail.time = Random.Range(0.2f, 0.5f);

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
        float duration = 0.05f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            trail.transform.position = Vector3.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        trail.transform.position = end;
        Destroy(trail.gameObject, trail.time);
    }
}
