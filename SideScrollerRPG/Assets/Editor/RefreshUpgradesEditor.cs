using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UpgradesSO))]
public class RefreshUpgradesEditor : Editor
{
    UpgradesSO upgrade;
    void OnEnable()
    {
        upgrade = (UpgradesSO)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("To Default"))
        {
            upgrade.ToDefault();
        }
    }

}
