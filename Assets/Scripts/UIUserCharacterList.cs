using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerProtocol;

public class UIUserCharacterList : MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private UserCharacterListController userCharacterListController;

    [Space(10)]

    [Header("UI Objects")]
    [SerializeField] private Button btnCreateCharacter;
    [SerializeField] private RectTransform characterList;
    [SerializeField] private GameObject userCharacterListItem;


    private void Awake()
    {
        userCharacterListController.OnChangeStep += (CreationStep step) =>
        {
            if (step == CreationStep.IDLE)
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
        userCharacterListController.ChangeStep(CreationStep.IDLE);

        btnCreateCharacter.onClick.AddListener(() =>
        {
            userCharacterListController.SetCharacterInfo(new UserCharacterInfo() { });
            userCharacterListController.ChangeStep(CreationStep.CUSTOMIZE);

        });



        foreach (Transform item in characterList.transform)
        {
            Destroy(item.gameObject);
        }

        userCharacterListController.FetchList();
        userCharacterListController.OnFetchCompleted += (userCharacters) =>
        {
            print("Fetch completed");
            foreach (UserCharacterInfo characterInfo in userCharacters)
            {
                GameObject item = Instantiate(userCharacterListItem, characterList.transform);
                item.GetComponent<UIUserCharacterListItem>().userCharacterInfo = characterInfo;
            }
        };

        userCharacterListController.OnRegisterNewCharacterCompleted += (UserCharacterInfo info) =>
        {
            GameObject item = Instantiate(userCharacterListItem, characterList.transform);
            item.GetComponent<UIUserCharacterListItem>().userCharacterInfo = info;
        };
    }
}
