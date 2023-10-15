using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class UIDialogBox : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas.transform.localScale = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        canvas.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBounce);
    }

    private void OnTriggerExit(Collider other)
    {

        canvas.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.OutBounce);
    }
}
