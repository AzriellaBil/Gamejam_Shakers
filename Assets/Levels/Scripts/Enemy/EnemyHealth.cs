using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] public int startingHealth = 40;
    public int currentHealth { get; private set;}

    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 )
        {
            Die();
        }
    }

    void Die()
    {
        Collider2D coll = GetComponent<Collider2D>();
        if (coll != null) coll.enabled = false;
        
        Destroy(gameObject);
    }

}
