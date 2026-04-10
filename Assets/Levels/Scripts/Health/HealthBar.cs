using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image currenthealthBar;
    void Start()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / playerHealth.startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateHealthBar()
    {
        float targetFill = playerHealth.currentHealth / playerHealth.startingHealth;

        LeanTween.cancel(gameObject); 

        LeanTween.value(gameObject, currenthealthBar.fillAmount, targetFill, 0.3f)
            .setOnUpdate((float val) => {
                currenthealthBar.fillAmount = val;
            })
            .setEase(LeanTweenType.easeOutQuad);
    }

}
