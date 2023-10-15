using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineDetect : MonoBehaviour
{
    // public NetworkManager networkManager;
    public NetworkController networkController;
    public string outlineLayerName = "Outline"; // Assign the outline layer name in the Inspector
    private int originalLayer;
    private bool isMouseOver = false;
    public ParticleSystem particle;
    private bool toggle = false;
    // Start is called before the first frame update
    void Start()
    {
        originalLayer = gameObject.layer;
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the mouse is still over the object
        if (isMouseOver)
        {
            ChangeLayer(outlineLayerName);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeLayer(outlineLayerName);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            ChangeLayer(LayerMask.LayerToName(originalLayer));
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            particle.gameObject.SetActive(true);
            networkController.io.D.Emit("heartUp");
        }
    }



    private void OnMouseDown()
    {
        toggle = true;
        particle.gameObject.SetActive(toggle);
        networkController.io.D.Emit("heartUp");
    }

    private void OnMouseEnter()
    {
        print("A");
        isMouseOver = true;
        ChangeLayer(outlineLayerName);
    }

    private void OnMouseExit()
    {
        print("B");
        isMouseOver = false;
        ChangeLayer(LayerMask.LayerToName(originalLayer));
    }

    private void ChangeLayer(string layerName)
    {
        // Check if the target layer exists
        int targetLayer = LayerMask.NameToLayer(layerName);
        if (targetLayer == -1)
        {
            Debug.LogError("Target layer does not exist.");
            return;
        }

        // Change the object's layer to the target layer
        gameObject.layer = targetLayer;

        Stack<Transform> stack = new Stack<Transform>();
        stack.Push(transform);

        while (stack.Count > 0)
        {
            Transform current = stack.Pop();
            current.gameObject.layer = targetLayer;

            // Push the children onto the stack to process them next
            foreach (Transform child in current)
            {
                if (child.name.Equals("heartUp")) continue;
                stack.Push(child);
            }
        }
    }
}
