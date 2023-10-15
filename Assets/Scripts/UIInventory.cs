using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private WheelMenuController wheelMenuController;
    [SerializeField] private RectTransform uiWheelMenu;
    private void Awake()
    {
        wheelMenuController.OnHoveredMenu += (ConstructionObject info) =>
        {
        };
        wheelMenuController.OnClickedMenu += (ConstructionObject info) =>
        {
            uiWheelMenu.DOScale(Vector3.zero, 0.25f).SetEase(Ease.OutCubic);
        };
    }
    // Start is called before the first frame update
    void Start()
    {
        uiWheelMenu.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        if (uiWheelMenu.localScale == Vector3.zero)
        {
            uiWheelMenu.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutCubic);

        }
        else
        {
            uiWheelMenu.DOScale(Vector3.zero, 0.25f).SetEase(Ease.OutCubic);
        }
    }
}
