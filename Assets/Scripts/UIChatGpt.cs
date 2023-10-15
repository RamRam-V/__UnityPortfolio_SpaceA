using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UIChatGpt : MonoBehaviour
{
    [SerializeField] private ChatGptController chatGptController;
    [SerializeField] private UserCharacterListController userCharacterListController;
    [SerializeField] private InputActionAsset inputActionAsset;
    private InputActionMap playerInputMap;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject list;
    [SerializeField] private GameObject item;
    [SerializeField] private Button attachmentBtn;
    private string fileUrl;

    [DllImport("__Internal")]
    private static extern void OpenPdfDialog();

    [SerializeField] private string serverURL;

    [Serializable]
    public class ResponseChatGpt
    {
        public string reply;
    }

    [Serializable]
    public class RequestChatGpt
    {
        public string message;
    }

    private void Awake()
    {
        attachmentBtn.onClick.AddListener(() =>
        {
            OpenPdfDialog();
        });

        chatGptController.OnChatGptPopup += (bool open) =>
        {
            if (open)
            {
                gameObject.SetActive(true);
            }
            else
            {
                transform.DOScale(Vector3.zero, 0.25f).onComplete += () =>
                    {
                        gameObject.SetActive(false);
                        playerInputMap.Enable();
                    };
            }
        };
        playerInputMap = inputActionAsset.FindActionMap("Player");


        inputField.onEndEdit.AddListener(OnSubmit);

        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);


    }

    void OnSubmit(string text)
    {
        DisplayText(userCharacterListController.GetCharacterInfo().nickname, text);

        inputField.text = "";
        StartCoroutine(SendMessageToServer(text));
    }

    public void SetPdf(string url)
    {
        fileUrl = url;
    }

    private void OnEnable()
    {
        transform.DOScale(Vector3.one, 0.25f);
        playerInputMap.Disable();
    }

    private IEnumerator SendMessageToServer(string message)
    {
        RequestChatGpt requestChatGpt = new RequestChatGpt()
        {
            message = message
        };

        using (var req = new UnityWebRequest(serverURL, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(requestChatGpt));
            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error While Sending: " + req.error);
            }
            else
            {
                // 응답 처리
                string responseText = req.downloadHandler.text;
                ResponseChatGpt responseChatGpt = JsonUtility.FromJson<ResponseChatGpt>(responseText);
                DisplayText("고양이NPC", responseChatGpt.reply);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            chatGptController.HidePopup();
        }
    }

    void DisplayText(string title, string content)
    {

        GameObject obj = Instantiate(item, list.transform);
        UIChatItem uIChatItem = obj.GetComponent<UIChatItem>();
        uIChatItem.SetTitle(title);
        uIChatItem.SetContent(content);
    }
}
