using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    [SerializeField] private AssetReference loadingScreenScnee;
    [SerializeField] private AssetReference loginScene;
    [SerializeField] private SceneLoadController sceneLoadController;
    [SerializeField] private NetworkController networkController;

    private AsyncOperationHandle handle;

    [SerializeField] private AssetReference serverScene;

    void Awake()
    {
        //로딩 스크린 씬 로드
        handle = loadingScreenScnee.LoadSceneAsync(LoadSceneMode.Additive);
        handle.Completed += (op) =>
        {
            if (networkController.isLocal)
            {
                sceneLoadController.LoadScene(loginScene);

            }
            else
            {
                sceneLoadController.LoadScene(serverScene);

            }

        };
    }
}
