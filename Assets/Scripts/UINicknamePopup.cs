using System.Collections;
using System.Collections.Generic;
using PlayerProtocol;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINicknamePopup : MonoBehaviour
{
    [SerializeField] private UserCharacterListController userCharacterListController;

    [SerializeField] private Button btnConfirm;
    [SerializeField] private TMP_InputField inputField;

    private void Awake()
    {
        userCharacterListController.OnChangeStep += (CreationStep step) =>
        {
            if (step == CreationStep.SET_NICKNAME)
            {
                gameObject.SetActive(true);
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

        btnConfirm.onClick.AddListener(() =>
        {
            UserCharacterInfo info = userCharacterListController.GetCharacterInfo();
            info.nickname = inputField.text;
            userCharacterListController.SetCharacterInfo(info);

            userCharacterListController.ChangeStep(CreationStep.IDLE);
            userCharacterListController.RegisterNewCharacter();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
