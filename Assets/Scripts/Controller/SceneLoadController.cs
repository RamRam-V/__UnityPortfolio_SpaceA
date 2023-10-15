using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Controller/SceneLoadController")]
public class SceneLoadController : ScriptableObject
{
    public event Action<AssetReference> OnLoadScene;

    public void LoadScene(AssetReference sceneRef)
    {
        OnLoadScene?.Invoke(sceneRef);
    }
}


