using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float healthAmount;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            healthAmount = maxHealth;
        }
        else
        {
            healthAmount = PlayerPrefs.GetFloat("PlayerHealth", maxHealth);
        }
        HealthBarManager.Instance.UpdateHealthBar(healthAmount, maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Heal(5);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, maxHealth);
        PlayerPrefs.SetFloat("PlayerHealth", healthAmount); 
        HealthBarManager.Instance.UpdateHealthBar(healthAmount, maxHealth);
        if (healthAmount <= 0)
        {
            SceneManager.LoadScene("Level 1");
        }
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, maxHealth);
        PlayerPrefs.SetFloat("PlayerHealth", healthAmount);
        HealthBarManager.Instance.UpdateHealthBar(healthAmount, maxHealth);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("PlayerHealth", healthAmount);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trap"))
        {
            Destroy(gameObject);
        }
    }
}
