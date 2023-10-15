using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using TMPro;

public class UIRoomInteraction : MonoBehaviour
{
    [SerializeField] private SceneLoadManager sceneLoadManager;
    [SerializeField] private RectTransform roomInteractionObj;
    [SerializeField] private Animator roomTitleAnim;
    [SerializeField] private TMP_Text textTitle;
    private AssetReference sceneRef;

    public void Open()
    {
        roomTitleAnim.SetBool("TitleOpen", true);
        roomInteractionObj.gameObject.SetActive(true);
    }

    public void Close()
    {
        roomTitleAnim.SetBool("TitleOpen", false);
        roomInteractionObj.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        roomTitleAnim.SetBool("TitleOpen", false);
        roomInteractionObj.gameObject.SetActive(false);
        sceneLoadManager.LoadScene(sceneRef);
    }

    public void SetWarpScene(AssetReference sceneRef, string title)
    {
        print("D");
        this.sceneRef = sceneRef;
        this.textTitle.text = title;
    }
}
