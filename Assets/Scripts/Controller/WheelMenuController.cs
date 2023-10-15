using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using PlayerProtocol;

public enum ConstructionType
{
    NPC_CAT,
    DISPLAY_IMAGE,
    DISPLAY_VIDEO
}

[Serializable]
public class ConstructionObject
{
    public ConstructionType type;
    public string name;
    public GameObject prefab;
}


[CreateAssetMenu(menuName = "Controller/WheelMenuController")]
public class WheelMenuController : ScriptableObject
{
    public event Action<ConstructionObject> OnHoveredMenu;
    public event Action<ConstructionObject> OnClickedMenu;
    public List<ConstructionObject> constructionObjects;

    public void HoveredMenu(int idx)
    {
        OnHoveredMenu?.Invoke(constructionObjects[idx]);
    }

    public void ClickedMenu(int idx)
    {
        OnClickedMenu?.Invoke(constructionObjects[idx]);
    }

    public ConstructionObject GetInfoByType(ConstructionType type)
    {
        foreach (var obj in constructionObjects)
        {
            if (obj.type == type)
            {
                return obj;
            }
        }
        return null;
    }


    public ConstructionObject GetInfoByIndex(int idx)
    {
        return constructionObjects[idx];
    }

    // public void SetCurrentConstruction(ConstructionObject constructionObject)
    // {
    //     currentlySelectedConstruction = constructionObject;
    // }
}