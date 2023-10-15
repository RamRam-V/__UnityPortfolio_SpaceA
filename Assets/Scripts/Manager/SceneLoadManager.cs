using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Unity.VisualScripting;

public class SceneLoadManager : MonoBehaviour
{
    public NetworkManager networkManager;

    // public event Action OnSceneLoadStart;
    // public event Action OnSceneLoadEnd;

    public Scene EntryWorld;
    private AsyncOperationHandle<SceneInstance>? lastLoadedSceneHandle;

    [SerializeField] private SceneLoadController sceneLoadController;
    [SerializeField] private LoadingScreenController loadingScreenController;
    private bool isLoading = false;

    // 게임 시작 시 월드 진입.
    void Start()
    {
        //에디터에서 플레이 버튼 누를 때 현재 씬이 InitScene에서 실행한 것이 아니라면 에디터 플레이 버튼 이벤트를 관리하는 LoadOnPlayMode 클래스에서 수행.
        // if (!SceneManager.GetActiveScene().name.Equals("InitScene"))
        // {
        //     OnSceneLoadEnd?.Invoke();
        //     return;
        // }

        //서버는 매뉴얼 빌드를 통해 각 씬에 대한 빌드 진행
        // if (!networkManager.isLocal)
        //     return;

        // OnSceneLoadStart?.Invoke();

        // sceneHandle = curSceneRef.LoadSceneAsync(LoadSceneMode.Additive);
        // sceneHandle.Completed += OnSceneLoaded;

        sceneLoadController.OnLoadScene += (sceneRef) => LoadScene(sceneRef);
    }

    private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            print("Scene Loaded");
            isLoading = false;

            SceneManager.SetActiveScene(handle.Result.Scene);

            loadingScreenController.EndLoadingAnim();
            loadingScreenController.SetPercent(100);
        }
        else
        {
            Debug.LogError("Failed to load scene: " + handle.OperationException);
        }
    }

    public void LoadScene(AssetReference scene)
    {

        loadingScreenController.StartLoadingAnim(() =>
        {
            if (lastLoadedSceneHandle.HasValue)
            {
                Addressables.UnloadSceneAsync(lastLoadedSceneHandle.Value, true).Completed += (handle) =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        Debug.Log("Scene Unloaded");
                        isLoading = true;

                        lastLoadedSceneHandle = scene.LoadSceneAsync(LoadSceneMode.Additive);
                        lastLoadedSceneHandle.Value.Completed += OnSceneLoaded;
                    }
                    else
                    {
                        Debug.LogError("Failed to unload scene: " + handle.OperationException);
                    }
                };
            }
            else
            {
                isLoading = true;

                lastLoadedSceneHandle = scene.LoadSceneAsync(LoadSceneMode.Additive);
                lastLoadedSceneHandle.Value.Completed += OnSceneLoaded;
            }
        });
    }

    void Update()
    {
        if (lastLoadedSceneHandle.HasValue && isLoading)
        {
            loadingScreenController.SetPercent((int)(lastLoadedSceneHandle.Value.PercentComplete * 100f));
        }
    }
}
