
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class locationUI : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    [SerializeField] float TweeenSpeed;

    public StringEventChannel onNewLocation;
    public LocationDatabase databaseReference;
    [SerializeField] TextMeshProUGUI L_ImageText;
    [SerializeField] TextMeshProUGUI L_ImageDesc;
    private bool IsPlaying = false;


    void Awake()
    {

    }
    void OnEnable()
    {
        if (onNewLocation == null)
    {
        Debug.LogError("onNewLocation is not assigned in the Inspector!", this);
        return;
    }
    onNewLocation.OnEventRaised += ShowLocationUI;
    }

    void OnDisable()
    {
       onNewLocation.OnEventRaised += ShowLocationUI;
    }

    void ShowLocationUI(string Locations)
    {
        if (IsPlaying == false)
        {
            IsPlaying = true;

            string L_Description = databaseReference.GetLocationDescription(Locations);
            L_ImageText.text = Locations;
            L_ImageDesc.text = L_Description;


            LeanTween.cancel(gameObject);

            LeanTween.alphaCanvas(cg, 1f, TweeenSpeed / 2)
            .setEase(LeanTweenType.easeInOutSine)
            .setOnComplete(HideUI);
        }
    }

    private void HideUI()
    {

        LeanTween.alphaCanvas(cg, 0f, TweeenSpeed / 2)
       .setEase(LeanTweenType.easeInOutSine)
       .setOnComplete(() =>
       {
           IsPlaying = false;
       });
        ;

    }
}
