using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloorBuilder))]
public class FloorBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var t = (FloorBuilder)target;
        if (GUILayout.Button("Build Floor Sides"))
        {
            for (int i = t.transform.childCount; i > 0; --i)
            {
                DestroyImmediate(t.transform.GetChild(0).gameObject);
            }

            t.BuildFloorSides();
        }
    }
}