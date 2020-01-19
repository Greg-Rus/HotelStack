using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FloorErector))]
public class FloorErectorEditor : Editor
{
    [SerializeField] public float Width;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        var t = (FloorErector)target;
        if (GUILayout.Button("Build Floor"))
        {

            //for (int i = t.transform.childCount; i > 1; --i)
            //{
            //    DestroyImmediate(t.transform.GetChild(1).gameObject);
            //}

            t.BuildFloor(t.Width, t.Depth);
        }

        if (GUILayout.Button("Split Floor"))
        {

            t.SplitFloor(t.SplitTransform.position, t.SplitTransform.rotation.eulerAngles);
        }
    }
}