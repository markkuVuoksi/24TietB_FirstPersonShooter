using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JA_PlayerHealth : MonoBehaviour, IDamageable_JA
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public Slider healthBar;
    public GameObject gameOverUI;
    public Image hurtFlashImage;
    public float flashDuration = 0.2f;
    public Color flashColor = new Color(1, 0, 0, 0.4f); // punainen 40 % alpha

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar)
            healthBar.maxValue = maxHealth;
            UpdateUI();
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        UpdateUI();
        StartCoroutine(HurtFlash());

        if (currentHealth <= 0f)
            Die();
    }

    IEnumerator HurtFlash()
    {
        if (hurtFlashImage == null) yield break;

        hurtFlashImage.color = flashColor;
        float t = 0f;
        while (t < flashDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(flashColor.a, 0f, t / flashDuration);
            hurtFlashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, a);
            yield return null;
        }
    }

    void UpdateUI()
    {
        if (healthBar)
            healthBar.value = currentHealth;
    }

    void Die()
    {
        Debug.Log("Player died");

        var cc = GetComponent<JA_playerMovement>();
        if (cc != null) cc.enabled = false;

        var gun = GetComponent<JA_Hitscan>();
        if (gun != null) gun.enabled = false;
        

        if (gameOverUI) gameOverUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}
