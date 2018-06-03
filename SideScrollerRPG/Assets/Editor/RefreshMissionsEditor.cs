using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MissionSO))]
public class RefreshMissionsEditor : Editor
{
    MissionSO mission;
    void OnEnable()
    {
        mission = (MissionSO)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("To Default"))
        {
            mission.ToDefault();
        }
    }

}
