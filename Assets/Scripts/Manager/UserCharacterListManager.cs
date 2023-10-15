using System;
using System.Collections;
using System.Collections.Generic;
using Firesplash.GameDevAssets.SocketIOPlus;
using PlayerProtocol;
using UnityEngine;
using UnityEngine.UI;

public class UserCharacterListManager : MonoBehaviour
{
    [SerializeField] private NetworkController networkController;
    [SerializeField] private UserCharacterListController userCharacterListController;
    [SerializeField] private LoginSO loginSO;

    void Start()
    {
        userCharacterListController.OnFetchList += () =>
        {
            networkController.io.D.Emit("FetchUserCharacters", loginSO.token);
        };

        networkController.io.D.On<string>("FetchUserCharactersCompleted", (payload) =>
        {
            List<UserCharacterInfo> userCharacterInfos = JsonHelper.FromJson<UserCharacterInfo>(payload);
            userCharacterListController.OnFetchCompleted?.Invoke(userCharacterInfos);
        });

        userCharacterListController.OnRegisterNewCharacter += (UserCharacterInfo info) =>
        {
            RegisterInfo registerInfo = new RegisterInfo()
            {
                token = loginSO.token,
                info = info
            };
            networkController.io.D.Emit("RegisterNewCharacter", registerInfo);
        };

        networkController.io.D.On<string>("RegisterNewCharacterCompleted", (payload) =>
        {
            RegisterInfo result = JsonUtility.FromJson<RegisterInfo>(payload);
            userCharacterListController.OnRegisterNewCharacterCompleted?.Invoke(result.info);
        });

    }
}
