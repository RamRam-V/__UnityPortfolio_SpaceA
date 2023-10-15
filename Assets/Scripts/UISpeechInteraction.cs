using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine;

public class UISpeechInteraction : MonoBehaviour
{
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private RectTransform obj;
    [SerializeField] private RectTransform obj2;
    [SerializeField] private EventSO eventSO;

    // [DllImport("__Internal")]
    // private static extern void OpenPdfDialog();

    // Start is called before the first frame update
    void Start()
    {
        // networkManager.io.D.On("nextPage", () =>
        // {
        //     eventSO.RaiseEvent();
        // });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Open()
    {
        obj.gameObject.SetActive(true);
    }

    public void Close()
    {
        obj.gameObject.SetActive(false);
    }

    public void Open2()
    {
        obj2.gameObject.SetActive(true);
        // networkManager.io.D.Emit("nextPage");
    }

    // public void OnClick()
    // {
    //     obj.gameObject.SetActive(false);
    //     OpenPdfDialog();
    // }

    public void OnClick2()
    {
        //send socket event: nextPage
        // networkManager.io.D.Emit("nextPage");
        eventSO.RaiseEvent();
    }
}
