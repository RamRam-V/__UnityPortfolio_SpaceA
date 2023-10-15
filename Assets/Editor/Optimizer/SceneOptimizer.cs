using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneOptimizer : MonoBehaviour
{
    public static void CreateCombinedMesh(GameObject go)
    {
        MeshFilter[] meshFilters = go.GetComponentsInChildren<MeshFilter>();
        List<Material> materials = new List<Material>();
        List<CombineInstance> combineInstancesList = new List<CombineInstance>();

        foreach (var _meshFilter in meshFilters)
        {
            MeshRenderer _meshRenderer = _meshFilter.GetComponent<MeshRenderer>();

            if (_meshFilter.sharedMesh == null || _meshRenderer == null || _meshRenderer.sharedMaterials.Length != _meshFilter.sharedMesh.subMeshCount)
            {
                continue;
            }

            for (int s = 0; s < _meshFilter.sharedMesh.subMeshCount; s++)
            {
                CombineInstance combineInstance = new CombineInstance();

                combineInstance.subMeshIndex = s;
                combineInstance.mesh = _meshFilter.sharedMesh;
                combineInstance.transform = _meshFilter.transform.localToWorldMatrix;
                combineInstancesList.Add(combineInstance);

                materials.Add(_meshRenderer.sharedMaterials[s]);
            }
        }

        // Create new GameObject to hold the combined mesh
        GameObject combinedObject = new GameObject("CombinedMesh");
        combinedObject.transform.position = go.transform.position;
        combinedObject.transform.rotation = go.transform.rotation;

        MeshFilter meshFilter = combinedObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = combinedObject.AddComponent<MeshRenderer>();

        Mesh combinedMesh = new Mesh();
        combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        if (combineInstancesList.Count > 0)
        {
            combinedMesh.CombineMeshes(combineInstancesList.ToArray(), false);
            meshFilter.sharedMesh = combinedMesh;
            meshRenderer.sharedMaterials = materials.ToArray();
        }
    }

    public static void DisableChildColliders(GameObject go)
    {
        foreach (Transform child in go.GetComponentsInChildren<Transform>())
        {
            foreach (Collider collider in child.GetComponentsInChildren<Collider>())
            {
                if (collider != null)
                {
                    collider.enabled = false;
                }
            }
        }

    }


    public static void EnableChildColliders(GameObject go)
    {
        foreach (Transform child in go.GetComponentsInChildren<Transform>())
        {
            foreach (Collider collider in child.GetComponentsInChildren<Collider>())
            {
                if (collider != null)
                {
                    collider.enabled = true;
                }
            }
        }
    }
}
