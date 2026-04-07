using GDX.DataTables.CellValues;
using UnityEngine;
using UnityEngine.UI;
public class locationUI : MonoBehaviour
{
    [SerializeField] CanvasGroup cg;
    [SerializeField] float TweeenSpeed;
    public FloatCellValue LocationCell;
    
    private void ShowLocationUI()
    {
        LeanTween.cancel(gameObject); 

        LeanTween.alphaCanvas(cg, 1f, TweeenSpeed/2)
        .setEase(LeanTweenType.easeInOutSine)
        .setOnComplete(HideUI);
    
    }

    private void HideUI()
    {
        LeanTween.alphaCanvas(cg,0f, TweeenSpeed/2)
        .setEase(LeanTweenType.easeInOutSine);
    }
}
