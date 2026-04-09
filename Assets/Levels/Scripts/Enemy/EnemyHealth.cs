using JetBrains.Annotations;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set;}
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        Debug.Log(gameObject.name + " Kena hit! HP sisa: " + currentHealth);

        if (currentHealth <= 0 )
        {
            Debug.Log("Musuh Mati!");
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
