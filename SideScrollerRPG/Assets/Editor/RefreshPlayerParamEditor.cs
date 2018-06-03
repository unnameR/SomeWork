using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerSO))]
public class RefreshPlayerParamEditor : Editor
{
    PlayerSO playerParam;
    void OnEnable()
    {
        playerParam = (PlayerSO)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("To Default"))
        {
            playerParam.ToDefault();
        }
    }

}
