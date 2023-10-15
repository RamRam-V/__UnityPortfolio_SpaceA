using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneOptimizerEditor : EditorWindow
{
    GameObject go;

    [MenuItem("Tools/SceneOptimizer")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(SceneOptimizerEditor), false, "Scene Optimizer");
    }

    void OnGUI()
    {
        GUILayout.Label("Scene Optimizer", EditorStyles.boldLabel);

        // Object field lets you choose an GameObject directly from Unity Editor
        go = (GameObject)EditorGUILayout.ObjectField("Target GameObject", go, typeof(GameObject), true);

        // Button for combining meshes
        if (GUILayout.Button("Create Combined Mesh"))
        {
            if (go != null)
            {
                SceneOptimizer.CreateCombinedMesh(go);
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "No GameObject selected. Please select a GameObject first.", "Ok");
            }
        }
        // Button for combining meshes
        if (GUILayout.Button("Disable Colliders"))
        {
            if (go != null)
            {
                SceneOptimizer.DisableChildColliders(go);
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "No GameObject selected. Please select a GameObject first.", "Ok");
            }
        }
        // Button for combining meshes
        if (GUILayout.Button("Enable Colliders"))
        {
            if (go != null)
            {
                SceneOptimizer.EnableChildColliders(go);
            }
            else
            {
                EditorUtility.DisplayDialog("Error", "No GameObject selected. Please select a GameObject first.", "Ok");
            }
        }
    }
}
