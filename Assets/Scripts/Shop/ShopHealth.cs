using UnityEngine;
using UnityEngine.UI;

public class ShopHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public Image healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            print("Loose Game");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemi"))
        {
            LooseHealth();
        }
    }

    private void LooseHealth()
    {
        currentHealth--;
        UpdateHealthBar();
    }

    public void GainHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
    }
}
