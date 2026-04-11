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
        Time.timeScale = 1f;
        LeanTween.alphaCanvas(cg, 0f, TweeenSpeed).setIgnoreTimeScale(true)
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
        Time.timeScale = 0f;
        LeanTween.cancel(gameObject); 

        LeanTween.alphaCanvas(cg, 1f, TweeenSpeed).setIgnoreTimeScale(true)
            .setEase(LeanTweenType.easeInOutSine);
    
    }

    public void QuitMenu()
    {
        if(IsPaused == true)
        {
                   SceneManager.LoadScene("Main Menu"); 
        }

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
