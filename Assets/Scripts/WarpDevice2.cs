using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.AddressableAssets;

public class WarpDevice2 : MonoBehaviour
{
    private CinemachineVirtualCamera vCam;
    [SerializeField] private AssetReference sceneRef;
    [SerializeField] private WarpEventSO warpEventSO;
    [SerializeField] private string title;

    // Start is called before the first frame update
    void Start()
    {
        vCam = transform.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        print("A");
        warpEventSO.RaiseEvent(true, sceneRef, title);
        vCam.Priority = 100;
    }
    private void OnTriggerExit(Collider other)
    {
        print("B");
        warpEventSO.RaiseEvent(false, null, "");
        vCam.Priority = 0;
    }
}
