using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have  no arguments (Example: Exit game event)
/// </summary>

[CreateAssetMenu(menuName = "Events/EventSO")]
public class EventSO : ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke();
    }
}


