using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public StringEventChannel PlayerLocation;
    [SerializeField] private Health healthScript;
    public float fadeDuration = 1f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Location"))
        {
            PlayerLocation.Raise(collision.gameObject.name);

            AudioSource audio = collision.GetComponent<AudioSource>();
            audio.volume = 0f;
            audio.enabled = true;

            LeanTween.value(gameObject, 0f, 1f, fadeDuration)
                .setOnUpdate((float val) => audio.volume = val);
        }
        if (collision.CompareTag("Void"))
        {
            healthScript.TakeDamage(healthScript.startingHealth, "Jatuh ke Void");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Location"))
        {
            AudioSource audio = collision.GetComponent<AudioSource>();
            if (audio == null) return;

            LeanTween.value(gameObject, 1f, 0f, fadeDuration)
                .setOnUpdate((float val) => audio.volume = val)
                .setOnComplete(() => audio.enabled = false);
        }
    }
}