
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DeathUI : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    [SerializeField] float TweeenSpeed;

    public FloatEventChannel HPChanges;


    void OnEnable()
    {
        if (HPChanges == null)
    {
        Debug.LogError("not assigned in the Inspector!", this);
        return;
    }
    HPChanges.OnEventRaised += ShowUI;
    }

    void OnDisable()
    {
       HPChanges.OnEventRaised -= ShowUI;
    }

    void ShowUI(float HPNum)
    {
        if (HPNum <= 0)
        {
            LeanTween.cancel(gameObject);

            LeanTween.alphaCanvas(cg, 1f, TweeenSpeed / 2)
            .setEase(LeanTweenType.easeInOutSine)
            .setOnComplete(HideUI);
        }
    }

    private void HideUI()
    {

        LeanTween.alphaCanvas(cg, 0f, TweeenSpeed / 2)
       .setEase(LeanTweenType.easeInOutSine);

    }
}
