using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class MeshToMeshCollider : MonoBehaviour
{

    [MenuItem("Tools/Mesh/Generate Mesh Collider")]
    static void MeshGen()
    {
        Transform[] selection = Selection.GetTransforms(SelectionMode.Editable | SelectionMode.ExcludePrefab);

        if (selection.Length <= 0)
        {
            EditorUtility.DisplayDialog("Nothing selected!", "No objects have been selected...", "Continue");
            return;
        }

        for (int i = 0; i < selection.Length; ++i)
        {
            if (selection[i].GetComponent<MeshFilter>())
            {
                Mesh originalMesh = selection[i].GetComponent<MeshFilter>().sharedMesh;

                int result = EditorUtility.DisplayDialogComplex("Mesh to Mesh Collider Settings", "What would you like to do to the selected mesh?", "Bake and apply", "Cancel", "Only apply");

                if (result == 0)
                {
                    string filePath = EditorUtility.SaveFilePanelInProject("Bake Collider", "Collider(" + i + ")", "asset", "");

                    if (filePath == "")
                    {
                        EditorUtility.DisplayDialog("Invalid file path!", "Please select a valid file path and try again.", "Continue");
                        return;
                    }

                    AssetDatabase.CreateAsset(selection[i].GetComponent<MeshFilter>().sharedMesh, filePath);

                    if (!selection[i].gameObject.GetComponent<MeshCollider>())
                    {
                        MeshCollider collider = selection[i].gameObject.AddComponent<MeshCollider>();
                        collider.sharedMesh = selection[i].GetComponent<MeshFilter>().sharedMesh;
                    }
                    else selection[i].gameObject.GetComponent<MeshCollider>().sharedMesh = selection[i].GetComponent<MeshFilter>().sharedMesh;
                }
                else if (result == 2)
                {
                    if (!selection[i].gameObject.GetComponent<MeshCollider>())
                    {
                        MeshCollider collider = selection[i].gameObject.AddComponent<MeshCollider>();
                        collider.sharedMesh = selection[i].GetComponent<MeshFilter>().sharedMesh;
                    }
                    else selection[i].gameObject.GetComponent<MeshCollider>().sharedMesh = selection[i].GetComponent<MeshFilter>().sharedMesh;
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Object does not contain a Mesh Filter", "When selecting objects to bake mesh capable of collision you must only select objetcs containing a Mesh Filter component with a valid mesh.", "Continue");
            }
        }
    }
}