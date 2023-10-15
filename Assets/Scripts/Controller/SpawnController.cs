using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using PlayerProtocol;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(menuName = "Controller/SpawnController")]
public class SpawnController : ScriptableObject
{
    [Serializable]
    public struct CharacterPrefabData
    {
        public CharacterType type;
        public GameObject gameObject;
    }
    [SerializeField] List<CharacterPrefabData> localCharacterPrefabData;
    [SerializeField] List<CharacterPrefabData> onlineCharacterPrefabData;

    public event Action<UserCharacterInfo> OnSpawnedLocalPlayer;
    public event Action<UserCharacterInfo> OnSpawnOnlinePlayer;
    public event Action<PlayerData> OnSpawnOnlineWithInfo;


    public GameObject GetLocalCharacterPrefab(CharacterType type)
    {
        foreach (var data in localCharacterPrefabData)
        {
            if (data.type == type)
                return data.gameObject;
        }

        return null;
    }

    public GameObject GetOnlineCharacterPrefab(CharacterType type)
    {
        foreach (var data in onlineCharacterPrefabData)
        {
            if (data.type == type)
                return data.gameObject;
        }

        return null;
    }

    public void SpawnedLocalPlayer(GameObject obj, UserCharacterInfo info)
    {
        OnSpawnedLocalPlayer?.Invoke(info);
    }

    public void SpawnOnlinePlayer(UserCharacterInfo info)
    {
        OnSpawnOnlinePlayer?.Invoke(info);
    }

    public void SpawnOnlinePlayer(PlayerData playerData)
    {
        OnSpawnOnlineWithInfo?.Invoke(playerData);
    }
}