using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSpawner : MonoBehaviour
{
    [SerializeField] private WheelMenuController wheelMenuController;

    // Start is called before the first frame update
    void Awake()
    {
        wheelMenuController.OnClickedMenu += (ConstructionObject info) =>
        {
            GameObject curConstruction = Instantiate(info.prefab, transform.TransformPoint(Vector3.forward * 2), transform.rotation);
            curConstruction.transform.LookAt(transform);
            curConstruction.transform.rotation = Quaternion.Euler(new Vector3(0, curConstruction.transform.rotation.eulerAngles.y, 0));
        };
    }

    // Update is called once per frame
    void Update()
    {

    }
}
