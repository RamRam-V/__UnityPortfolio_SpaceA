using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class UIUploadButton : MonoBehaviour
{
    [SerializeField] private DisplayButtonController displayButtonController;


    [DllImport("__Internal")]
    private static extern void OpenImageDialog();

    [DllImport("__Internal")]
    private static extern void OpenVideoDialog();

    private string mode;

    [SerializeField] private Button btn;
    private void Awake()
    {
        displayButtonController.OnShowButton += (bool open, string type) =>
        {
            if (open)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
            mode = type;
        };
        btn.onClick.AddListener(() =>
        {
            if (mode.Equals("image"))
            {
                OpenImageDialog();
            }
            else if (mode.Equals("video"))
            {
                OpenVideoDialog();
            }
        });
        gameObject.SetActive(false);
    }

    public void Load(string type)
    {
        print("type> " + type);
        if (type.Equals("image"))
        {
            displayButtonController.LoadImage(type);
        }
        else if (type.Equals("video"))
        {
            displayButtonController.LoadVideo(type);
        }

    }
}
