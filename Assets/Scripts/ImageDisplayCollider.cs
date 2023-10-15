using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ImageDisplayCollider : MonoBehaviour
{

    [SerializeField] private DisplayButtonController displayButtonController;
    [SerializeField] private string type;
    public Image img = null;
    public VideoPlayer videoPlayer = null;
    public MeshRenderer mat;

    private void Awake()
    {
        displayButtonController.OnLoadVideo += (string type) =>
        {
            if (type.Equals("video"))
            {
                if (videoPlayer != null)
                {
                    mat.material.color = Color.white;
                    videoPlayer.Play();
                }

            }
        };

        displayButtonController.OnLoadImage += (string type) =>
        {
            if (type.Equals("image"))
            {
                if (img != null)
                    img.color = Color.white;
            }
        };

    }

    private void OnTriggerEnter(Collider other)
    {
        displayButtonController.ShowButton(type);
    }
    private void OnTriggerExit(Collider other)
    {
        displayButtonController.HideButton(type);
    }
}
