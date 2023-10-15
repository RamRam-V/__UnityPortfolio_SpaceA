using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using PlayerProtocol;


[CreateAssetMenu(menuName = "Controller/UserCharacterController")]
public class UserCharacterListController : ScriptableObject
{
    [SerializeField] private LoginSO loginSO;
    [SerializeField] private UserCharacterInfo characterInfo;
    private List<KeyValuePair<CharacterType, string>> typeNameList;

    public Action OnFetchList;
    public Action<List<UserCharacterInfo>> OnFetchCompleted;

    public Action<CharacterType> OnShowCharacter;
    public Action<CreationStep> OnChangeStep;
    public Action<string> OnChangedCharacterType;
    public Action<UserCharacterInfo> OnRegisterNewCharacter;
    public Action<UserCharacterInfo> OnRegisterNewCharacterCompleted;
    public Action<UserCharacterInfo> OnShowConfirmPopup;

    private void OnEnable()
    {
        characterInfo = new UserCharacterInfo();

        typeNameList = new List<KeyValuePair<CharacterType, string>>
        {
            new KeyValuePair<CharacterType, string>(CharacterType.ECO, "Eco"),
            new KeyValuePair<CharacterType, string>(CharacterType.LEON, "Leon"),
            new KeyValuePair<CharacterType, string>(CharacterType.AMELI, "Ameli")
        };
    }

    public void ShowConfirmPopup(UserCharacterInfo info)
    {
        OnShowConfirmPopup?.Invoke(info);
    }

    public void FetchList()
    {
        OnFetchList?.Invoke();
    }

    public void RegisterNewCharacter()
    {
        OnRegisterNewCharacter?.Invoke(characterInfo);
    }

    public void ShowCharacter(CharacterType type)
    {
        OnShowCharacter?.Invoke(type);
    }

    public void ShowCharacter(int idx)
    {
        OnShowCharacter?.Invoke(typeNameList[idx].Key);
    }


    public void ChangeStep(CreationStep step)
    {
        OnChangeStep?.Invoke(step);
    }

    public void SetCharacterInfo(UserCharacterInfo info)
    {
        characterInfo = info;
    }

    public UserCharacterInfo GetCharacterInfo()
    {
        return characterInfo;
    }

    public string GetCharaterTypeName(CharacterType type)
    {
        foreach (KeyValuePair<CharacterType, string> typeName in typeNameList)
        {
            if (typeName.Key.Equals(type))
            {
                return typeName.Value;
            }
        }

        return "";
    }

    public string GetCharaterTypeName(int idx)
    {
        return typeNameList[idx].Value;
    }

    public int GetTypeNameLength()
    {
        return typeNameList.Count;
    }
}