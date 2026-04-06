
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;

    [SerializeField] private Image currenthealthBar;
    void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / playerHealth.startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / playerHealth.startingHealth;
    }
}
