using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class UILoginButton : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void HandleLogin(string id, string pw);

    public AssetReference sceneRef = null;

    [SerializeField] private LoginSO loginData;

    [SerializeField] private NetworkController networkController;

    private bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickLogin()
    {
        if (flag) return;

#if UNITY_EDITOR //에디터 환경
        networkController.ConnectClient(sceneRef);
#else //브라우저 환경
        HandleLogin(loginData.id, loginData.pw);
#endif

        flag = true;
    }

    public void AuthComplete(string token)
    {
        loginData.id = "";
        loginData.pw = "";
        loginData.token = token;
        networkController.ConnectClient(sceneRef);
    }

    public void AuthFailed()
    {
        print("auth failed");
    }
}
