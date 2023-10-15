using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class UIWheelMenu : MonoBehaviour
{
    [SerializeField] private WheelMenuController wheelMenuController;

    [SerializeField] private RectTransform background;
    [SerializeField] private GameObject uiRadialBg;
    [SerializeField] private TMP_Text indicatorText;
    [SerializeField] private RectTransform radius;

    private int segments = 3;
    private int curIdx = -1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isMousePosInCircle())
        {
            uiRadialBg.SetActive(true);

            Vector2 delta = background.position - Input.mousePosition;
            float angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
            angle += 90;

            if (angle < 0)
                angle += 360;

            int segment = 0;
            int part = 360 / segments;
            for (int i = 0; i < 360; i += part)
            {
                if (Mathf.Clamp(angle, i, i + part) == angle)
                {
                    indicatorText.text = wheelMenuController.GetInfoByIndex(segment).name;
                    Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, i + part));
                    uiRadialBg.transform.DORotateQuaternion(targetRotation, 0.25f).SetEase(Ease.OutCubic); // 0.25f is the animation duration
                    if (curIdx != segment)
                    {
                        wheelMenuController.HoveredMenu(segment);
                        curIdx = segment;
                    }
                    break;
                }
                segment++;
            }

            if (Input.GetMouseButtonDown(0))
            {
                wheelMenuController.ClickedMenu(segment);
            }
        }
        else
        {
            uiRadialBg.SetActive(false);
            indicatorText.text = "";
            curIdx = -1;
        }
    }

    bool isMousePosInCircle()
    {
        return Vector3.Distance(radius.position, background.transform.position) >= Vector3.Distance(Input.mousePosition, background.position);
    }
}
