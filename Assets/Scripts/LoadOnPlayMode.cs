using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

// #if UNITY_EDITOR && !UNITY_SERVER
// [InitializeOnLoad]
// public static class LoadOnPlayMode
// {
//     static LoadOnPlayMode()
//     {
//         EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
//     }

//     private static void OnPlayModeStateChanged(PlayModeStateChange state)
//     {
//         string curSceneName = SceneManager.GetActiveScene().name;
//         if (state == PlayModeStateChange.EnteredPlayMode && !curSceneName.Equals("InitScene"))
//         {
//             // Load the default scene when entering play mode
//             EditorSceneManager.LoadScene("InitScene", LoadSceneMode.Additive);

//         }
//     }
// }
// #endif