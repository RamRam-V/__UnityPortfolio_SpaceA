using System;
using System.Collections;
using System.Collections.Generic;
using PlayerProtocol;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICustomizingMenu : MonoBehaviour
{
    [SerializeField] private UserCharacterListController userCharacterListController;

    private int idx = 0;

    [SerializeField] private Button btnLeftArrow;
    [SerializeField] private Button btnRightArrow;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button btnNext;

    private void Awake()
    {
        userCharacterListController.OnChangeStep += (CreationStep step) =>
        {
            if (step == CreationStep.CUSTOMIZE)
            {
                gameObject.SetActive(true);

                idx = 0;
                text.text = userCharacterListController.GetCharaterTypeName(idx);
                userCharacterListController.ShowCharacter(idx);
            }
            else
            {
                gameObject.SetActive(false);
            }
        };
    }
    // Start is called before the first frame update
    void Start()
    {

        btnLeftArrow.onClick.AddListener(() =>
        {
            idx -= 1;

            if (idx < 0)
            {
                idx = userCharacterListController.GetTypeNameLength() - 1;
            }
            text.text = userCharacterListController.GetCharaterTypeName(idx);
            userCharacterListController.ShowCharacter(idx);
        });

        btnRightArrow.onClick.AddListener(() =>
        {
            idx += 1;

            if (idx >= userCharacterListController.GetTypeNameLength())
            {
                idx = 0;
            }
            text.text = userCharacterListController.GetCharaterTypeName(idx);
            userCharacterListController.ShowCharacter(idx);
        });

        btnNext.onClick.AddListener(() =>
        {
            UserCharacterInfo info = userCharacterListController.GetCharacterInfo();
            info.type = (CharacterType)idx;
            userCharacterListController.SetCharacterInfo(info);

            userCharacterListController.ChangeStep(CreationStep.SET_NICKNAME);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
