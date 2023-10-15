using System;
using System.Collections.Generic;
using Cinemachine;
using PlayerProtocol;
using UnityEngine;


[CreateAssetMenu(menuName = "Controller/NetworkObjectController")]
public class PlayerNetworkObjectController : ScriptableObject
{
    public event Action<string> OnServerSideUpdate;
    // public event Action<List<PlayerData>> OnUpdateTrasnfrom;
    public event Action<PlayerData> OnPlayerTransformUpdate;

    public UserCharacterInfo playerInfo;
    public Transform playerTransform;
    public CinemachineVirtualCamera playerVCam;
    public PlayerAnimationData playerAnimationData;

    public Dictionary<string, PlayerNetworkObject> players;

    private void OnEnable()
    {
        if (players != null) players.Clear();
        players = new Dictionary<string, PlayerNetworkObject>();
    }

    public void PlayerTransformUpdate(PlayerData playerData)
    {
        OnPlayerTransformUpdate?.Invoke(playerData);
    }

    // public void UpdateAllTransform(List<PlayerData> transformDatas)
    // {
    //     OnUpdateTrasnfrom?.Invoke(transformDatas);
    // }

    public void SetPlayerNetworkObject(UserCharacterInfo info, Transform transform, CinemachineVirtualCamera vCam)
    {
        playerTransform = transform;
        playerInfo = info;
        playerVCam = vCam;
    }

    public void AddPlayerObject(string name, PlayerNetworkObject player)
    {
        players.Add(name, player);
    }

    public void RemovePlayerObject(string name)
    {
        players.Remove(name);
    }
}
