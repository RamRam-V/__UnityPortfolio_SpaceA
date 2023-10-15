using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatGptCollider : MonoBehaviour
{
    [SerializeField] private ChatGptController chatGptController;

    private void OnTriggerEnter(Collider other)
    {
        chatGptController.OpenPopup();
    }

    private void OnTriggerExit(Collider other)
    {
        chatGptController.HidePopup();
    }
}
