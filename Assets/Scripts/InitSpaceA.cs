using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSpaceA : MonoBehaviour
{
    [SerializeField] private NetworkController networkController;
    private void Start()
    {
#if UNITY_SERVER
        networkController.ConnectServer();
#endif

    }
}
