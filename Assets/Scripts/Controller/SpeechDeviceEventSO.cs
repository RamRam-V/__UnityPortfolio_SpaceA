using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have no arguments (Example: Exit game event)
/// </summary>

[CreateAssetMenu(menuName = "Events/SpeechDeviceEventSO")]
public class SpeechDeviceEventSO : ScriptableObject
{
    public event Action<bool, int> OnEventRaised;

    public void RaiseEvent(bool val, int idx)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(val, idx);
    }
}


