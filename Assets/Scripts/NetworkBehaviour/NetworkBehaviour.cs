using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firesplash.GameDevAssets.SocketIOPlus;
using Cinemachine;
using System;

public class NetworkBehaviour : MonoBehaviour
{
    [HideInInspector]
    public NetworkController networkController; //SpawnManager에 의해 주입.

    [HideInInspector]
    public string socketId; //추후 scriptableobject로 플레이어 정보 교체. 서버에 의해 주입.

    [HideInInspector]
    public bool isMe; //player 객체들 중에서 내가 조작하고 있는 객체인지.

    public CinemachineVirtualCamera vCam;



    protected virtual void Update()
    {

    }


}
