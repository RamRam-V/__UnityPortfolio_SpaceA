using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILoadingScreen : MonoBehaviour
{
    public event Action OnAnimStart;
    public event Action OnAnimEnd;

    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text textPercent;

    [SerializeField] private LoadingScreenController loadingScreenController;
    [SerializeField] private GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        loadingScreenController.OnEndLoadingAnim += () => EndLoadingAnim();
        loadingScreenController.OnStartLoadingAnim += (callback) => { StartLoadingAnim(); OnAnimStart += callback; };
        loadingScreenController.OnSetPercent += (percent) => SetPercent(percent);
    }

    public void StartLoadingAnim()
    {
        cam.SetActive(true);
        animator.SetBool("loading", true);
    }

    public void EndLoadingAnim()
    {
        animator.SetBool("loading", false);
    }

    public void SetPercent(int percent)
    {
        textPercent.text = percent.ToString() + "%";
    }

    //anim event
    private void OnFadeInStarted()
    {
        OnAnimStart?.Invoke();
        OnAnimStart = null;
    }

    //anim event
    private void OnFadeOutCompleted()
    {
        cam.SetActive(false);
        OnAnimEnd?.Invoke();

        textPercent.text = "0%";
    }
}
