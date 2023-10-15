using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private WarpEventSO warpEventSO;
    [SerializeField] private UIRoomInteraction uiRoomInteraction;

    // [SerializeField] private SpeechDeviceEventSO speechEventSO;
    [SerializeField] private UISpeechInteraction uiSpeechInteraction;


    // Start is called before the first frame update
    void Start()
    {
        warpEventSO.OnEventRaised += (val, sceneRef, title) =>
        {
            print("C");
            if (val)
            {
                uiRoomInteraction.Open();
            }
            else
            {
                uiRoomInteraction.Close();
            }
            uiRoomInteraction.SetWarpScene(sceneRef, title);
        };

        // speechEventSO.OnEventRaised += (val, idx) =>
        // {
        //     if (val)
        //     {
        //         if (idx == 1)
        //             uiSpeechInteraction.Open();
        //         else
        //             uiSpeechInteraction.Open2();
        //     }
        //     else
        //     {
        //         uiSpeechInteraction.Close();
        //     }
        // };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
