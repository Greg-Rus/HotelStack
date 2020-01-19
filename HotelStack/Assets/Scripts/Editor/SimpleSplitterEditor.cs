using UnityEditor;
using UnityEngine;

namespace MeshSplitting.Splitters
{
    [CustomEditor(typeof(SimpleSplitter))]
    public class SimpleSplitterEditor : Editor
    {
        [SerializeField] public float Width;
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var t = (SimpleSplitter)target;
            if (GUILayout.Button("Split Floor"))
            {
                t.SplitObject();
            }
        }
    }
}