using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloorSideBuilder))]
public class FloorSideBuilderEditor : Editor
{
    [SerializeField] public float Width;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var t = (FloorSideBuilder)target;
        if (GUILayout.Button("Build Object"))
        {

            for (int i = t.transform.childCount; i > 0; --i)
            {
                DestroyImmediate(t.transform.GetChild(0).gameObject);
            }

            t.BuildFloorSide(t.Length);
        }
    }
}