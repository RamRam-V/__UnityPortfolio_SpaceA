using System.Collections;
using System.Collections.Generic;
using Firesplash.GameDevAssets.SocketIOPlus;
using Player;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.InputSystem;
using PlayerProtocol;
using System.Runtime.CompilerServices;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private NetworkController networkController;

    private void Awake()
    {
        networkController.OnSetEvent += () =>
        {
            spawnController.OnSpawnedLocalPlayer += (UserCharacterInfo info) =>
            {
                string payload = JsonUtility.ToJson(info);
                networkController.io.D.Emit("SpawnPlayer", payload);
            };

            networkController.io.D.On<string>("SpawnPlayer", (payload) =>
            {
                print("ASFASF");
                UserCharacterInfo info = JsonUtility.FromJson<UserCharacterInfo>(payload);
                spawnController.SpawnOnlinePlayer(info);
            });
        };

    }

    // public SceneLoadManager sceneLoadManager;
    // public NetworkManager networkManager;
    // public Transform spawnPoint;
    // public GameObject playerLocalObj;
    // public GameObject playerOnlineObj;

    // private PlayerNetwork playerMe;
    // public List<PlayerNetwork> players;
    // public GameObject heartUI;
    // int tmp = 0;
    // bool stopTmp = false;

    // [SerializeField] private NetworkController networkController;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     // sceneLoadManager.OnSceneLoadStart += () =>
    //     // {
    //     //     DestroyAllPlayer();
    //     //     stopTmp = true;
    //     // };

    //     // sceneLoadManager.OnSceneLoadEnd += () =>
    //     // {
    //     //     spawnPoint = GameObject.Find("SpawnPoint").transform;
    //     //     if (networkManager.isLocal)
    //     //     {
    //     //         StartCoroutine(SpawnDelay(networkManager.mySocketId, 1f));
    //     //     }
    //     //     tmp++;
    //     // };

    //     //client
    //     if (networkController.isLocal)
    //     {
    //         networkManager.OnSocketEventClient += (eventName, payload) =>
    //         {
    //             if (stopTmp) return;
    //             if (eventName.Equals("AllPlayerTransform"))
    //             {
    //                 List<PlayerData> players = JsonHelper.FromJson<PlayerData>(payload);

    //                 foreach (var player in players)
    //                 {
    //                     if (networkController.mySocketId.Equals(player.socketId))
    //                         continue;

    //                     PlayerNetwork targetPlayer = GetPlayerObj(player.socketId);
    //                     if (targetPlayer == null)
    //                     {
    //                         GameObject obj = Spawn(player.socketId, true);
    //                         obj.transform.position = player.transformData.position;
    //                         obj.transform.rotation = player.transformData.rotation;
    //                     }
    //                     else
    //                     {
    //                         targetPlayer.MoveTo(player.transformData);

    //                         targetPlayer.animData = player.animationData;

    //                         //걷기
    //                         if (targetPlayer.animData.isMoving)
    //                         {
    //                             if (!targetPlayer.animData.isSprint)
    //                             {

    //                                 targetPlayer.animator.SetFloat("Speed", 2);
    //                                 targetPlayer.animator.SetFloat("MotionSpeed", 1);
    //                             }
    //                             else
    //                             {

    //                                 targetPlayer.animator.SetFloat("Speed", 6);
    //                                 targetPlayer.animator.SetFloat("MotionSpeed", 1);
    //                             }
    //                         }
    //                         //뛰기
    //                         else if (targetPlayer.animData.isMoving && targetPlayer.animData.isSprint)
    //                         {
    //                         }
    //                         else if (!targetPlayer.animData.isMoving)
    //                         {
    //                             targetPlayer.animator.SetFloat("Speed", 0);
    //                         }
    //                         //점프
    //                         targetPlayer.animator.SetBool("Jump", targetPlayer.animData.isJump);
    //                         targetPlayer.animator.SetBool("Grounded", targetPlayer.animData.isGrounded);
    //                     }
    //                 }
    //             }
    //         };
    //     }

    //     //server update
    //     networkManager.OnTickServer += (deltaTime) =>
    //     {
    //         //server
    //         if (networkManager.IsConnected() && !networkController.isLocal)
    //         {
    //             List<PlayerData> allPlayerData = new();
    //             foreach (var player in players)
    //             {
    //                 PlayerData playerData = new()
    //                 {
    //                     socketId = player.socketId,
    //                     transformData = new()
    //                     {
    //                         position = player.transform.position,
    //                         rotation = player.transform.rotation,
    //                         camRot = player.vCam.Follow.transform.rotation
    //                     },
    //                     animationData = player.animData
    //                 };
    //                 allPlayerData.Add(playerData);
    //             }
    //             string strjson = JsonHelper.ToJson(allPlayerData);
    //             networkController.io.D.Emit("AllPlayerTransform", strjson);
    //         }
    //     };
    // }

    // IEnumerator SpawnDelay(string socketid, float seconds)
    // {
    //     yield return new WaitForSeconds(seconds);
    //     Spawn(socketid);

    //     spawnPoint = GameObject.Find("SpawnPoint").transform;

    //     // playerMe.gameObject.SetActive(true);
    //     playerMe.transform.root.transform.position = spawnPoint.transform.position;
    //     heartUI.SetActive(true);
    //     stopTmp = false;
    // }

    // public GameObject Spawn(string socketId, bool isOtherPlayer = false)
    // {
    //     // PlayerNetwork me = GetPlayerObj(networkManager.mySocketId);
    //     // if (me != null)
    //     // {
    //     //     return me.gameObject;
    //     // }

    //     GameObject obj = Instantiate(isOtherPlayer == false ? playerLocalObj : playerOnlineObj, spawnPoint.position, spawnPoint.rotation);
    //     obj.name = socketId;

    //     PlayerNetwork objNetwork = obj.transform.Find("PlayerArmature").GetComponent<PlayerNetwork>();
    //     objNetwork.networkController = networkController;
    //     objNetwork.socketId = socketId;
    //     objNetwork.isMe = !isOtherPlayer;
    //     players.Add(objNetwork);
    //     if (!isOtherPlayer)
    //         playerMe = objNetwork;

    //     if (isOtherPlayer)
    //     {
    //         PlayerInput playerInput = obj.transform.Find("PlayerArmature").GetComponent<PlayerInput>();
    //         if (playerInput != null)
    //             obj.transform.Find("PlayerArmature").GetComponent<PlayerInput>().enabled = false;
    //     }

    //     CinemachineVirtualCamera cam = obj.transform.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
    //     if (networkController.isLocal)
    //     {
    //         if (!isOtherPlayer)
    //         {
    //             if (tmp < 2)
    //                 networkController.io.D.Emit("SpawnPlayer", socketId);
    //             cam.Priority = 11;
    //         }
    //         else
    //             cam.Priority = 0;
    //     }
    //     else
    //     {
    //         cam.Priority = 0;
    //     }

    //     return obj;
    // }

    // public PlayerNetwork GetPlayerObj(string socketId)
    // {
    //     foreach (var player in players)
    //     {
    //         if (player.socketId.Equals(socketId))
    //         {
    //             return player;
    //         }
    //     }

    //     return null;
    // }

    // public void DestroyPlayer(string socketId)
    // {
    //     for (int i = 0; i < players.Count; i++)
    //     {
    //         if (players[i].socketId.Equals(socketId))
    //         {
    //             Destroy(players[i].transform.root.gameObject);
    //             players.RemoveAt(i);
    //             break;
    //         }
    //     }
    // }

    // public void DestroyAllPlayer()
    // {
    //     for (int i = 0; i < players.Count; i++)
    //     {
    //         // if (players[i].socketId.Equals(networkManager.mySocketId))
    //         //     continue;
    //         Destroy(players[i].transform.root.gameObject);
    //         players.RemoveAt(i);
    //     }
    // }
}
