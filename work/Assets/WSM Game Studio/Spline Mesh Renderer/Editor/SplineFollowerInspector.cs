using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using WSMGameStudio.Splines;

[CustomEditor(typeof(SplineFollower))]
public class SplineFollowerInspector : Editor
{
    private SplineFollower _splineFollower;

    private GUIContent _btnMoveToStart = new GUIContent("Move to Start Position", "Move object to start position along the spline");

    public override void OnInspectorGUI()
    {
        _splineFollower = target as SplineFollower;

        base.DrawDefaultInspector();

        if (GUILayout.Button(_btnMoveToStart))
        {
            _splineFollower.MoveToStartPosition();
            MarkSceneAlteration();
        }
    }

    /// <summary>
    /// Show player the scene needs to be saved
    /// </summary>
    private void MarkSceneAlteration()
    {
        if (!Application.isPlaying)
        {
            EditorUtility.SetDirty(_splineFollower);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
