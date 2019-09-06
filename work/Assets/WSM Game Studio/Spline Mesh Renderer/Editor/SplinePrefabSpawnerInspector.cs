using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using WSMGameStudio.Splines;

[CustomEditor(typeof(SplinePrefabSpawner))]
public class SplinePrefabSpawnerInspector : Editor
{
    private SplinePrefabSpawner _splinePrefabSpawner;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _splinePrefabSpawner = (SplinePrefabSpawner)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Spawn"))
        {
            _splinePrefabSpawner.SpawnPrefabs();
            MarkSceneAlteration();
        }

        if (GUILayout.Button("Reset"))
        {
            _splinePrefabSpawner.ResetObjects();
            MarkSceneAlteration();
        }

        GUILayout.EndHorizontal();
    }

    /// <summary>
    /// Show player the scene needs to be saved
    /// </summary>
    private void MarkSceneAlteration()
    {
        if (!Application.isPlaying)
        {
            EditorUtility.SetDirty(_splinePrefabSpawner);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
