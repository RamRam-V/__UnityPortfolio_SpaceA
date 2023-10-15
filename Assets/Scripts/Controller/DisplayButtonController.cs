using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Controller/DisplayButtonController")]
public class DisplayButtonController : ScriptableObject
{
    public event Action<bool, string> OnShowButton;
    public event Action<string> OnLoadVideo;
    public event Action<string> OnLoadImage;

    public void ShowButton(string type)
    {
        OnShowButton?.Invoke(true, type);
    }
    public void HideButton(string type)
    {
        OnShowButton?.Invoke(false, type);
    }

    public void LoadVideo(string url)
    {
        OnLoadVideo?.Invoke(url);
    }

    public void LoadImage(string url)
    {
        OnLoadImage?.Invoke(url);
    }
}
