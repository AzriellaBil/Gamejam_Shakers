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

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0 , startingHealth);
        //currentHealth -= _damage;

        if (currentHealth > 0 )
        {
            
        }
        else
        {
            
        }

    }



}
