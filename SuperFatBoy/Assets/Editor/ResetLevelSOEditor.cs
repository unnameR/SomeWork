using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameLevelSO))]
public class ResetLevelSOEditor : Editor
{
    GameLevelSO mission;
    void OnEnable()
    {
        mission = (GameLevelSO)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Reset Level"))
        {
            mission.ResetLevel();
        }
    }

}
