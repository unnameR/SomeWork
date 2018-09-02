using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameLevelSO))]
public class ResetLevelSOEditor : Editor
{
    GameLevelSO level;
    void OnEnable()
    {
        level = (GameLevelSO)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Reset Level"))
        {
            level.ResetLevel();
        }
        //EditorUtility.SetDirty(level);
    }

}
