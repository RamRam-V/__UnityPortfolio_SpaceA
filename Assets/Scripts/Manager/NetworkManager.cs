using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firesplash.GameDevAssets.SocketIOPlus;
using System;
using Unity.Burst.Intrinsics;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEditor;
using PlayerProtocol;

public class NetworkManager : MonoBehaviour
{
    public SceneLoadManager sceneLoadManager;
    public SocketIOClient io;
    public SpawnManager spawnManager;
    public event Action<float> OnTickClient;
    public event Action<float> OnTickServer;
    public event Action<string, string> OnSocketEventClient;
    public event Action<string, string> OnSocketEventServer;

    public bool clientAuthoritativeMovement = true;

    public string serverSceneName;

    private float elapsedTime = 0;

    [SerializeField] private LoginSO loginData;

    [SerializeField] private NetworkController networkController;

    [Serializable]
    struct AuthData
    {
        public string token;
    }

    private void Awake()
    {

        networkController.io = io;
    }

    // Start is called before the first frame update
    private void Start()
    {

        networkController.OnConnectClient += (sceneRef) => ConnectClient(sceneRef);
        networkController.OnConnectServer += () => ConnectServer();

        //client, server
        networkController.io.D.On("connect", () =>
        {
            Debug.Log("LOCAL: Hey, we are connected!");
        });

        //client, server
        networkController.io.D.On("disconnect", (payload) =>
        {
            Debug.Log("disconnect > Disconnected from server. " + payload);
        });

        //client, server
        networkController.io.D.On<string>("clientDisconnected", (socketId) =>
        {
            print("clientDisconnected > " + socketId);
            // spawnManager.DestroyPlayer(socketId);
        });

        networkController.SetEvents();
    }

    public void ConnectClient(AssetReference sceneRef)
    {
        networkController.io.D.OnAny<string>((eventName, payload) =>
                        {
                            OnSocketEventClient?.Invoke(eventName, payload);
                        });


        networkController.io.D.On<string>("connected", (socketId) =>
        {
            networkController.mySocketId = socketId;
            sceneLoadManager.LoadScene(sceneRef);
        });

        networkController.io.SetAuthPayloadCallback((namespacePath) =>
        {
            if (namespacePath.Equals("/"))
            {
                Debug.Log("Delivering auth data for namespace /");
                return new AuthData()
                {
                    token = loginData.token
                };
            }
            return null;
        });

#if UNITY_EDITOR || (!UNITY_EDITOR && UNITY_WEBGL && DEVELOPMENT_BUILD) //에디터 환경 또는 로컬 웹 브라우저 환경
        networkController.io.Connect("http://localhost:5000");
#elif !UNITY_EDITOR && UNITY_WEBGL && !DEVELOPMENT_BUILD //배포 웹 브라우저 환경
        networkController.io.Connect("https://ramramv.xyz");
#endif
    }

    public void ConnectServer()
    {
        networkController.io.D.OnAny<string>((eventName, payload) =>
                   {
                       OnSocketEventServer?.Invoke(eventName, payload);
                   });

        // networkController.io.D.On<string>("SpawnPlayer", (socketId) =>
        // {
        //     // spawnManager.Spawn(socketId, true);
        //     print("Spawned Player > " + socketId);
        // });

#if UNITY_EDITOR || (!UNITY_EDITOR && UNITY_SERVER && DEVELOPMENT_BUILD) //에디터 환경 또는 로컬 환경
        networkController.io.Connect("http://localhost:5000/" + serverSceneName + "/");
#elif !UNITY_EDITOR && UNITY_SERVER && !DEVELOPMENT_BUILD //배포 환경
        networkController.io.Connect("http://socket:5000/" + serverSceneName + "/");
#endif
    }

    private void Update()
    {
        // elapsedTime += Time.deltaTime;
        // if (elapsedTime > tickRate)
        // {
        //     if (isLocal)
        //     {
        //         OnTickClient?.Invoke(Time.deltaTime);
        //     }
        //     else
        //     {
        //         OnTickServer?.Invoke(Time.deltaTime);
        //     }

        //     elapsedTime = 0;
        // }
    }

    public bool IsConnected()
    {
        return networkController.io.State == Firesplash.GameDevAssets.SocketIOPlus.EngineIO.DataTypes.ConnectionState.Open;
    }

    private void OnApplicationQuit()
    {
        if (networkController.io && IsConnected())
            networkController.io.Disconnect();
    }

}
