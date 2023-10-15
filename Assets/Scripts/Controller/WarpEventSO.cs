using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have no arguments (Example: Exit game event)
/// </summary>

[CreateAssetMenu(menuName = "Events/WarpEventSO")]
public class WarpEventSO : ScriptableObject
{
    public event Action<bool, AssetReference, string> OnEventRaised;

    public void RaiseEvent(bool val, AssetReference sceneRef, string title)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(val, sceneRef, title);
    }
}


