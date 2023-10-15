using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have  no arguments (Example: Exit game event)
/// </summary>

[CreateAssetMenu(menuName = "Events/BoolEventSO")]
public class BoolEventSO : ScriptableObject
{
    public event Action<bool> OnEventRaised;

    public void RaiseEvent(bool val)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(val);
    }
}


