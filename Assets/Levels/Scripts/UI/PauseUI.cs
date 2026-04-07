using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] float TweeenSpeed;
    [SerializeField] CanvasGroup cg;
    private bool IsPaused = false;
    public void Continue()
    {
      LeanTween.alphaCanvas(cg, 0f, TweeenSpeed)
        .setEase(LeanTweenType.easeInOutSine)
        .setOnComplete(() =>
        {
            IsPaused = false;
        }
        );
    }
    public void Pause()
    {
        IsPaused = true;
        LeanTween.cancel(gameObject); 

        LeanTween.alphaCanvas(cg, 1f, TweeenSpeed)
        .setEase(LeanTweenType.easeInOutSine);
    
    }
        void Update()
    {
            if (Input.GetKeyDown(KeyCode.Escape))
                if(IsPaused == false)
                {
                    Pause();
                }
                else
                {
                    Continue();
                }
    }


}
