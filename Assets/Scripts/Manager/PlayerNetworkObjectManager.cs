using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using PlayerProtocol;
using UnityEngine;

public class PlayerNetworkObjectManager : MonoBehaviour
{
    [SerializeField] private NetworkController networkController;
    [SerializeField] private PlayerNetworkObjectController networkObjectController;
    [SerializeField] private SpawnController spawnController;

    private float elapsedTime = 0;

    private void Awake()
    {
        networkController.OnSetEvent += () =>
        {
            //server
            networkController.io.D.On<string>("PlayerTransform", (payload) =>
            {
                PlayerData playerData = JsonUtility.FromJson<PlayerData>(payload);
                networkObjectController.PlayerTransformUpdate(playerData);
            });

            //client
            networkController.io.D.On<string>("AllPlayerTransform", (payload) =>
            {
                if (networkObjectController.players.Count == 0)
                    return;

                List<PlayerData> transformDatas = JsonHelper.FromJson<PlayerData>(payload);
                foreach (PlayerData playerData in transformDatas)
                {
                    if (playerData.info.nickname.Equals(""))
                        continue;
                    if (networkObjectController.playerInfo.nickname.Equals(playerData.info.nickname))
                        continue;

                    if (networkObjectController.players.ContainsKey(playerData.info.nickname))
                    {
                        networkObjectController.PlayerTransformUpdate(playerData);
                    }
                    else
                    {
                        //spawn
                        spawnController.SpawnOnlinePlayer(playerData);
                    }
                }
            });
        };
    }

    // Update is called once per frame
    void Update()
    {

        if (networkObjectController.players.Count > 0 && elapsedTime >= networkController.tickRate)
        {
            elapsedTime = 0;

            //local
            if (networkController.isLocal)
            {
                CinemachineVirtualCamera playerVCam = networkObjectController.playerVCam;
                Transform playerTransform = networkObjectController.playerTransform;

                PlayerTransformData playerTransformData = new PlayerTransformData()
                {
                    position = playerTransform.position,
                    rotation = playerTransform.rotation,
                    camRot = playerVCam.Follow.transform.rotation
                };
                PlayerData playerData = new PlayerData()
                {
                    info = networkObjectController.playerInfo,
                    animationData = networkObjectController.playerAnimationData,
                    transformData = playerTransformData
                };

                string payload = JsonUtility.ToJson(playerData);
                networkController.io.D.Emit("PlayerTransform", payload);
            }
            //server
            else
            {
                List<PlayerData> playerDatas = new List<PlayerData>();

                foreach (var player in networkObjectController.players)
                {
                    playerDatas.Add(player.Value.playerData);
                }

                string payload = JsonHelper.ToJson(playerDatas);
                networkController.io.D.Emit("AllPlayerTransform", payload);
            }
        }
        elapsedTime += Time.deltaTime;
    }
}
