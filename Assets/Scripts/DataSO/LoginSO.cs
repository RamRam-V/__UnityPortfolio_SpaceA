using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

/// <summary>
/// This class is used for Events that have no arguments (Example: Exit game event)
/// </summary>

[CreateAssetMenu(menuName = "GameData/LoginSO")]
public class LoginSO : ScriptableObject
{
    public string id;
    public string pw;
    public string token;

    public void OnInputId(string id)
    {
        this.id = id;
    }

    public void OnInputPw(string pw)
    {
        this.pw = pw;
    }
}