using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Controller/LoadingScreenController")]
public class LoadingScreenController : ScriptableObject
{
    public event Action<Action> OnStartLoadingAnim;
    public event Action OnEndLoadingAnim;
    public event Action<int> OnSetPercent;

    public void StartLoadingAnim(Action callback)
    {
        OnStartLoadingAnim?.Invoke(callback);
    }

    public void EndLoadingAnim()
    {
        OnEndLoadingAnim?.Invoke();
    }

    public void SetPercent(int percent)
    {
        OnSetPercent?.Invoke(percent);
    }
}