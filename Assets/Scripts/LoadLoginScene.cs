using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class LoadLoginScene : MonoBehaviour
{
    public AssetReference managerSceneRef;
    // public AssetReference serverSceneRef = null;

    // public UILoadingScreen uiLoadingScreen;

    // private AsyncOperationHandle handle;

    // Start is called before the first frame update
    void Start()
    {
        managerSceneRef.LoadSceneAsync(LoadSceneMode.Single);

        //         uiLoadingScreen.StartLoadingAnim();
        //         uiLoadingScreen.OnAnimStartCompleted += () =>
        //         {
        // #if UNITY_SERVER
        //         handle =  serverSceneRef.LoadSceneAsync(LoadSceneMode.Single);
        // #else
        //             handle = sceneRef.LoadSceneAsync(LoadSceneMode.Additive);
        // #endif
        //             handle.Completed += (op) =>
        //             {
        //                 uiLoadingScreen.EndLoadingAnim();
        //                 uiLoadingScreen.SetPercent(100);

        //             };
        //         };
        //         uiLoadingScreen.OnAnimEndCompleted += () =>
        //         {
        //             SceneManager.UnloadSceneAsync(0);
        //         };
    }

    // Update is called once per frame
    void Update()
    {
        // if (!handle.IsDone)
        // {
        //     print(handle.PercentComplete);
        //     uiLoadingScreen.SetPercent((int)(handle.PercentComplete * 100f));
        // }
    }
}

