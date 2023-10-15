using System.Collections;
using System.Collections.Generic;
using PlayerProtocol;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class UIConfirmPopup : MonoBehaviour
{
    [SerializeField] private UserCharacterListController userCharacterListController;
    [SerializeField] private SceneLoadController sceneLoadController;
    [SerializeField] private AssetReference sceneRef;

    [SerializeField] private Button btnConfirm;
    [SerializeField] private Button btnCancle;
    [SerializeField] private TMP_Text text;
    private string srcText;

    private void Awake()
    {
        srcText = text.text;

        userCharacterListController.OnShowConfirmPopup += (UserCharacterInfo info) =>
        {
            gameObject.SetActive(true);
            text.text = text.text.Replace("{NAME}", info.nickname);
        };

        btnConfirm.onClick.AddListener(() =>
        {
            sceneLoadController.LoadScene(sceneRef);
        });

        btnCancle.onClick.AddListener(() =>
        {
            text.text = srcText;
            gameObject.SetActive(false);
        });

        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
