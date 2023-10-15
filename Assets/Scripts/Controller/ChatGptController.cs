using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Controller/ChatGptController")]
public class ChatGptController : ScriptableObject
{
    public event Action<bool> OnChatGptPopup;

    public void OpenPopup()
    {
        OnChatGptPopup?.Invoke(true);
    }
    public void HidePopup()
    {
        OnChatGptPopup?.Invoke(false);
    }
}
