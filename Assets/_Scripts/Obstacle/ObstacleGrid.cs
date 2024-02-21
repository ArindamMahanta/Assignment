using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "ObstacleGrid", menuName = "ScriptableObjects/ObstacleGrid")]
[System.Serializable]
public class ObstacleGrid : ScriptableObject
{
    [SerializeField] private bool[] obstacleGrid = new bool[100]; // Serialized 1D array

    /// <summary>
    /// Gets the boolean value of the specified position on the obstacle grid
    /// </summary>
    /// <param name="x">row of obstacle grid</param>
    /// <param name="y">column of obstacle grid</param>
    /// <returns></returns>
    public bool GetObstacleStatus(int x, int y)
    {
        return obstacleGrid[y * 10 + x];
    }

    /// <summary>
    /// Sets the boolean value to the specified position on the obstacle grid
    /// </summary>
    /// <param name="x">row of obstacle grid</param>
    /// <param name="y">column of obstacle grid</param>
    /// <param name="value">boolean value to set</param>
    public void SetObstacleStatus(int x, int y, bool value)
    {
        obstacleGrid[y * 10 + x] = value;
    }

#if UNITY_EDITOR
    // This method will be called whenever changes are made in the editor
    public void OnValidate()
    {
        EditorUtility.SetDirty(this); 
    }
#endif
}

[CustomEditor(typeof(ObstacleGrid))]
public class ObstacleGridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var grid = (ObstacleGrid)target;
        if (grid == null) return;

        GUILayout.Space(10);
        GUILayout.Label("Toggle Obstacles:");

        EditorGUI.BeginChangeCheck();
        for (int y = 0; y < 10; y++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int x = 0; x < 10; x++)
            {
                // Using GetObstacle and SetObstacle methods to interact with the 2D grid
                grid.SetObstacleStatus(x, y, EditorGUILayout.Toggle(grid.GetObstacleStatus(x, y), GUILayout.Width(20)));
            }

            EditorGUILayout.EndHorizontal();
        }

        if (EditorGUI.EndChangeCheck())
        {
            #if UNITY_EDITOR
            EditorUtility.SetDirty(grid);
            AssetDatabase.SaveAssets();
            #endif
        }
    }
}
