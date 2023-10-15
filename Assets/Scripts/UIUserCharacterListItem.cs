using System.Collections;
using System.Collections.Generic;
using PlayerProtocol;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUserCharacterListItem : MonoBehaviour
{
    [SerializeField] private UserCharacterListController userCharacterListController;

    public UserCharacterInfo userCharacterInfo;
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = userCharacterInfo.nickname;
        button.onClick.AddListener(() =>
        {
            userCharacterListController.SetCharacterInfo(userCharacterInfo);
            userCharacterListController.ShowCharacter(userCharacterInfo.type);
            userCharacterListController.ShowConfirmPopup(userCharacterInfo);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
