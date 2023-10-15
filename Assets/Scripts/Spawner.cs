using System.Collections;
using System.Collections.Generic;
using PlayerProtocol;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private UserCharacterListController userCharacterListController;
    [SerializeField] private SpawnController spawnController;
    [SerializeField] private NetworkController networkController;

    private void Start()
    {
        if (networkController.isLocal)
        {
            UserCharacterInfo info = userCharacterListController.GetCharacterInfo();
            GameObject gameObject = spawnController.GetLocalCharacterPrefab(info.type);
            GameObject myCharacter = Instantiate(gameObject, transform.position, transform.rotation);
            myCharacter.name = info.nickname;

            spawnController.SpawnedLocalPlayer(myCharacter, info);
        }

        spawnController.OnSpawnOnlinePlayer += (UserCharacterInfo info) =>
        {
            GameObject gameObject = spawnController.GetOnlineCharacterPrefab(info.type);
            GameObject obj = Instantiate(gameObject, transform.position, transform.rotation);
            obj.name = info.nickname;
        };

        spawnController.OnSpawnOnlineWithInfo += (PlayerData playerData) =>
        {
            GameObject gameObject = spawnController.GetOnlineCharacterPrefab(playerData.info.type);
            GameObject obj = Instantiate(gameObject, playerData.transformData.position, playerData.transformData.rotation);
            obj.name = playerData.info.nickname;
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
