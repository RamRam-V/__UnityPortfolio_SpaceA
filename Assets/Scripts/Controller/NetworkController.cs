using System;
using Firesplash.GameDevAssets.SocketIOPlus;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.InputSystem.Interactions;

[CreateAssetMenu(menuName = "Controller/NetworkController")]
public class NetworkController : ScriptableObject
{
    public SocketIOClient io;
    public event Action<AssetReference> OnConnectClient;
    public event Action OnConnectServer;
    public event Action OnSetEvent;

    public bool isLocal = true;
    public float tickRate = 0.05f;
    public string mySocketId = "";

    private void OnEnable()
    {
#if UNITY_SERVER
        isLocal = false;
#else
        isLocal = true;
#endif
        mySocketId = "";
    }

    public void ConnectClient(AssetReference sceneRef)
    {
        OnConnectClient?.Invoke(sceneRef);
    }

    public void ConnectServer()
    {
        OnConnectServer?.Invoke();
    }

    public void SetEvents()
    {
        OnSetEvent?.Invoke();
    }
}