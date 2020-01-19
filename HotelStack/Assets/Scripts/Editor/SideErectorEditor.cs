using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SideErector))]
public class SideErectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var t = (SideErector)target;
        if (GUILayout.Button("Build Object"))
        {
            t.MakeSide(t.Length);
        }
    }
}