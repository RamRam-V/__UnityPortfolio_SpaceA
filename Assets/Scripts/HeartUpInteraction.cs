using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUpInteraction : MonoBehaviour
{
    // public NetworkManager networkManager;
    public NetworkController networkController;
    public TMPro.TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        networkController.io.D.On("heartUp", () =>
        {
            int num = int.Parse(text.text);
            num += 3;
            text.text = num.ToString();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
