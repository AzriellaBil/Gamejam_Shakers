using JetBrains.Annotations;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public float startingHealth = 100f;

    [Header("Referensi")]
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private PlayerRespawn respawnScript;

    public float currentHealth { get; private set;}

    private bool isDead = false;

    void Start()
    {
        currentHealth = startingHealth;
        if (respawnScript == null)
            respawnScript = GetComponent<PlayerRespawn>();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0 , startingHealth);
        
        if (healthBar != null)
            healthBar.UpdateHealthBar();
        
        if (currentHealth <= 0 )
        {
            isDead = true;
            if (respawnScript != null)
                respawnScript.TriggerDeath("Mati dibantai Alien.");
        }
    }

    public void ResetHealth()
    {
        isDead = false;
        currentHealth = startingHealth;
        if (healthBar != null)
            healthBar.UpdateHealthBar();
    }

}
