using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image Outline;
    [SerializeField] float TweenSpeed;
    [SerializeField] CanvasGroup cg;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cg.alpha == 1)
        {
            LeanTween.cancel(gameObject);
            LeanTween.value(gameObject, 0f, 1f, TweenSpeed)
                .setOnUpdate((float val) =>
                {
                    Color c = Outline.color;
                    c.a = val;
                    Outline.color = c;
                });
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cg.alpha == 1)
        {
            LeanTween.cancel(gameObject);
            LeanTween.value(gameObject, 1f, 0f, TweenSpeed)
                .setOnUpdate((float val) =>
                {
                    Color c = Outline.color;
                    c.a = val;
                    Outline.color = c;
                });
        }
    }

    void OnDestroy()
    {
        LeanTween.cancel(gameObject);
    }
}