using System.Collections;
using System.Collections.Generic;
using PlayerProtocol;
using UnityEngine;

public class UserCharacterDisplay : MonoBehaviour
{
    [SerializeField] private UserCharacterListController userCharacterListController;
    [SerializeField] private List<GameObject> characters;

    // Start is called before the first frame update
    void Start()
    {
        userCharacterListController.OnShowCharacter += (CharacterType type) =>
        {
            foreach (var character in characters)
            {
                character.SetActive(false);
            }
            switch (type)
            {
                case CharacterType.ECO:
                    characters[0].SetActive(true);
                    break;
                case CharacterType.LEON:
                    characters[1].SetActive(true);
                    break;
                case CharacterType.AMELI:
                    characters[2].SetActive(true);
                    break;
                default:
                    break;
            }
        };
    }
}
