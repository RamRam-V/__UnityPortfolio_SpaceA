using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechDevice : MonoBehaviour
{
    [SerializeField] private EventSO screenEventSO;

    [SerializeField] private SpeechDeviceEventSO eventSO;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vCam;

    private int idx = 0;

    [SerializeField] private List<Sprite> imgs;
    [SerializeField] private GameObject screen;
    [SerializeField] private Image img;
    [SerializeField] private Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        screenEventSO.OnEventRaised += () =>
        {

            StartCoroutine(DelayOpen());
        };
    }

    IEnumerator DelayOpen()
    {
        yield return new WaitForSeconds(0.3f);
        canvas.gameObject.SetActive(true);
        screen.SetActive(false);
        img.sprite = imgs[idx++];
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        vCam.Priority = 100;
        eventSO.RaiseEvent(true, 1);
    }

    private void OnTriggerExit(Collider other)
    {
        vCam.Priority = 0;
        eventSO.RaiseEvent(false, 1);
    }

    public void LoadPdf()
    {
        print("LOAD!!");
        eventSO.RaiseEvent(true, 2);

        canvas.gameObject.SetActive(true);
        screen.SetActive(false);
        img.sprite = imgs[idx++];
    }
}
