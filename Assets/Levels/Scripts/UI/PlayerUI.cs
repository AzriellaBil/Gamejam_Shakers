using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public StringEventChannel PlayerLocation;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Location"))
            PlayerLocation.Raise(collision.gameObject.name);
    }
}
