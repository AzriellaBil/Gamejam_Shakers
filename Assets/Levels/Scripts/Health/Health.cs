using JetBrains.Annotations;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public float startingHealth;
    [SerializeField] private HealthBar healthBar;
    public float currentHealth { get; private set;}
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0 , startingHealth);
        healthBar.UpdateHealthBar();
        //currentHealth -= _damage;

        if (currentHealth > 0 )
        {
            
        }
        else
        {
            
        }

    }



}
